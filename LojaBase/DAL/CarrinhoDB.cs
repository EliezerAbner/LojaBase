using LojaBase.Models;
using LojaBase.Services;
using MySqlConnector;

namespace LojaBase.DAL
{
    public class CarrinhoDB
    {
        //Responsável pelas queries relacionadas ao carrinho

        private static string? _conn;
        private DBService? _dbService;

        public CarrinhoDB()
        {
            _dbService = new DBService();
            _conn = _dbService.LoadJson();
        }

        public void AdicionarAoCarrinho(Carrinho novoItem)
        {
            try
            {
                using var connection = new MySqlConnection(_conn);

                connection.Open();
                using var command = new MySqlCommand("INSERT INTO tbcarrinho (dataAdicao, quantidade, produtoId, usuarioId) VALUES (@dataAdicao, @quantidade, @produtoId, @usuarioId)", connection);
                command.Parameters.AddWithValue("@dataAdicao", novoItem.DataAdicao);
                command.Parameters.AddWithValue("@quantidade", novoItem.Quantidade);
                command.Parameters.AddWithValue("@produtoId", novoItem.ProdutoId);
                command.Parameters.AddWithValue("@usuarioId", novoItem.UsuarioId);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoverDoCarrinho(int carrinhoId)
        {
            try
            {
                using var connection = new MySqlConnection(_conn);

                connection.Open();
                using var command = new MySqlCommand("DELETE FROM tbCarrinho WHERE carrinhoId = @carrinhoId");
                command.Parameters.AddWithValue("@carrinhoId", carrinhoId);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Carrinho> ItensCarrinho(int usuarioId)
        {
            List<Carrinho> itens = new List<Carrinho>();

            try
            {
                using var connection = new MySqlConnection(_conn);

                connection.Open();
                using var command = new MySqlCommand("SELECT C.carrinhoId, C.dataAdicao, C.produtoId, P.nomeProduto, P.imagem, C.quantidade, C.usuarioId FROM tbCarrinho C INNER JOIN tbProduto p ON P.produtoId = C.produtoId WHERE usuarioId = @usuarioId", connection);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Carrinho item = new Carrinho()
                    {
                        Id = reader.GetInt32(0),
                        DataAdicao = reader.GetDateTime(1),
                        ProdutoId = reader.GetInt32(2),
                        NomeProduto = reader.GetString(3),
                        Imagem = reader.GetString(4),
                        Quantidade = reader.GetInt32(5),
                        UsuarioId = reader.GetInt32(6)
                    };

                    itens.Add(item);
                }
                connection.Close();
                return itens;
            }
            catch
            {
                return itens;
            }
        }
    }
}
