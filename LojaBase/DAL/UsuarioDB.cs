using LojaBase.Models;
using LojaBase.Services;
using MySqlConnector;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace LojaBase.DAL
{
    public class UsuarioDB
    {
        //Responsável pelas queries relacionadas ao usuario

        private static string? _conn;
        private DBService? _dbService;

        public UsuarioDB() 
        {
            _dbService = new DBService();
            _conn = _dbService.LoadJson();
        }

        public void NovoUsuario(Usuario usuario, Endereco end) 
        {
            using var connection = new MySqlConnection(_conn);

            connection.Open();
            using var command = new MySqlCommand("CALL cadastro_usuario(@nomeUsuario, @adm, @dataNascimento, @cpf, @telefone, @email, @senha, @rua, @numero, @cep, @bairro, @nomeMunicipio, @uf)", connection);

            command.Parameters.AddWithValue("@nomeUsuario", usuario.Nome);
            command.Parameters.AddWithValue("@adm", usuario.Adm);
            command.Parameters.AddWithValue("@dataNascimento", usuario.DataNascimento);
            command.Parameters.AddWithValue("@cpf", usuario.Cpf);
            command.Parameters.AddWithValue("@telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@email", usuario.Telefone);
            command.Parameters.AddWithValue("@senha", usuario.Senha);
            command.Parameters.AddWithValue("@rua", end.Logradouro);
            command.Parameters.AddWithValue("@numero", end.Numero);
            command.Parameters.AddWithValue("@cep", end.Cep);
            command.Parameters.AddWithValue("@bairro", end.Bairro);
            command.Parameters.AddWithValue("@nomeMunicipio", end.Localidade);
            command.Parameters.AddWithValue("@uf", end.Uf);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Usuario> ListaUsuarios([Optional] bool adm) 
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string query;
            
            if (adm)
            {
                query = "SELECT usuarioId, nomeUsuario, dataNascimento, cpf, adm FROM tbUsuario WHERE adm=1 and status=1";
            }
            else
            {
                query = "SELECT usuarioId, nomeUsuario, dataNascimento, cpf, adm FROM tbUsuario WHERE status=1";
            }

            using var connection = new MySqlConnection(_conn);
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Usuario usu = new Usuario()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    DataNascimento = reader.GetDateTime(2),
                    Cpf = reader.GetString(3),
                    Adm = reader.GetInt32(4)
                };

                listaUsuarios.Add(usu);
            }
            connection.Close();
            return listaUsuarios;
        }

        public List<Endereco> ListaEndereco(int usuarioId)
        {
            List<Endereco> listaEnderecos = new List<Endereco>();

            try
            {
                using var connection = new MySqlConnection(_conn);
                connection.Open();
                using var command = new MySqlCommand(

                    "SELECT enderecoId, cep, rua, numero, bairro, nomeMunicipio, usuarioId, Uf " +
                    "FROM tbEndereco " +
                    "INNER JOIN tbmunicipio M on M.municipioId = ende.municipioId" +
                    "WHERE usuarioId=@usuarioId", connection);
                
                command.Parameters.AddWithValue("@usuarioId", usuarioId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Endereco end = new Endereco()
                    {
                        EnderecoId = reader.GetInt32(0),
                        Cep = reader.GetString(1),
                        Logradouro = reader.GetString(2),
                        Numero = reader.GetInt32(3),
                        Bairro = reader.GetString(4),
                        Localidade = reader.GetString(5),
                        UsuarioId = reader.GetInt32(6),
                        Uf = reader.GetString(7)
                    };

                    listaEnderecos.Add(end);
                }
                connection.Close();
                return listaEnderecos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Email> ListaEmails(int usuarioId)
        {
            try
            {
                List<Email> listaEmails = new List<Email>();

                using var connection = new MySqlConnection(_conn);
                connection.Open();
                using var command = new MySqlCommand("SELECT emailId, email, usuarioId FROM tbEmail WHERE usuarioId=@usuarioId", connection);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email()
                    {
                        EmailId = reader.GetInt32(0),
                        EnderecoEmail = reader.GetString(1),
                        UsuarioId = reader.GetInt32(2)
                    };

                    listaEmails.Add(email);
                }
                connection.Close();

                return listaEmails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaCpf(string cpf)
        {
            string? resultado = "";

            using var connection = new MySqlConnection(_conn);
            connection.Open();
            
            using var command = new MySqlCommand("SELECT TOP 1 cpf FROM tbUsuario WHERE cpf= @cpf and status = 1", connection);
            command.Parameters.AddWithValue("@cpf", cpf);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                resultado = reader.GetString(0);
            }
            connection.Close();

            if (resultado == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool VerificaEmail(string email)
        {
            string? resultado = "";

            using var connection = new MySqlConnection(_conn);
            connection.Open();

            using var command = new MySqlCommand("SELECT TOP 1 email FROM tbEmail WHERE email = @email and status = 1", connection);
            command.Parameters.AddWithValue("@email", email);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                resultado = reader.GetString(0);
            }
            connection.Close();

            if (resultado == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}