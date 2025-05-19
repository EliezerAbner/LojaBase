using LojaBase.Models;
using LojaBase.Services;
using MySqlConnector;

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

        public List<Usuario> ListaUsuarios() 
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            
            using var connection = new MySqlConnection(_conn);

            connection.Open();
            using var command = new MySqlCommand("SELECT * FROM tbUsuario WHERE status=1",connection);
            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Usuario usu = new Usuario()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Adm = reader.GetInt32(2),
                    DataNascimento = reader.GetDateTime(3),
                    Cpf = reader.GetString(5)
                };

                listaUsuarios.Add(usu);
            }
            connection.Close();
            return listaUsuarios;
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