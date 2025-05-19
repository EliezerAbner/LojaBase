using LojaBase.Services;
using LojaBase.Models;
using MySqlConnector;

namespace LojaBase.DAL
{
    public class ProdutoDB
    {
        private static string? _conn;
        private DBService? _dbService;

        public ProdutoDB()
        {
            _dbService = new DBService();
            _conn = _dbService.LoadJson();
        }

        public void CadastrarProduto(Produto produto) 
        {

        }

        public List<Categoria> listaCategorias() 
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using var connection = new MySqlConnection(_conn);

            connection.Open();
            using var command = new MySqlCommand("SELECT categoriaId, nomeCategoria, descricao, icone FROM tbCategoria", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Categoria cat = new Categoria()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    Icone = reader.GetString(3)
                };
                listaCategorias.Add(cat);
            }
            connection.Close();
            return listaCategorias;
        }

        public List<Produto> ListaProdutos()
        {
            List<Produto> listaProdutos = new List<Produto>();

            using var connection = new MySqlConnection(_conn);

            connection.Open();
            using var command = new MySqlCommand("SELECT produtoId,nomeProduto, descricao, preco, imagem FROM tbProduto WHERE status = 1", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Produto prod = new Produto()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    Preco = Convert.ToDecimal(reader.GetDecimal(3)),
                    Imagem = reader.GetString(4) 
                    //Quantidade = Convert.ToDouble(reader.GetString(5)),
                    //TipoQuantidade = reader.GetString(6)
                };
                listaProdutos.Add(prod);
            }
            connection.Close();
            return listaProdutos;
        }
    }
}
