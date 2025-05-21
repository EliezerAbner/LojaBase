using LojaBase.Services;
using LojaBase.Models;
using MySqlConnector;
using System.Runtime.InteropServices;

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

            try
            {
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
            }
            catch (Exception ex) 
            {
                return listaProdutos;
            }

            return listaProdutos;
        }

        public List<Produto> BuscaProdutos(int categoriaId, [Optional] string? nomeProduto) 
        {
            List<Produto> buscaProdutos = new List<Produto>();
            MySqlCommand command;

            using var connection = new MySqlConnection(_conn);

            try
            {
                connection.Open();

                if (nomeProduto != null)
                {
                    nomeProduto = String.Concat("%", nomeProduto, "%");

                    command = new MySqlCommand("SELECT produtoId,nomeProduto, descricao, preco, imagem FROM tbProduto WHERE nomeProduto like @pesquisa", connection);
                    command.Parameters.AddWithValue("@pesquisa", nomeProduto);
                }
                else
                {
                    command = new MySqlCommand("SELECT produtoId,nomeProduto, descricao, preco, imagem FROM tbProduto WHERE categoriaId = @categoriaId", connection);
                    command.Parameters.AddWithValue("@categoriaId", categoriaId);
                }

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
                    buscaProdutos.Add(prod);
                }
                connection.Close();
                return buscaProdutos;
            }
            catch (Exception ex)
            {
                return buscaProdutos;
            }   
        }

        public Produto BuscaProduto(int produtoId)
        {
            Produto produto = new Produto();
            MySqlCommand command;

            using var connection = new MySqlConnection(_conn);

            try
            {
                connection.Open();
                command = new MySqlCommand("SELECT produtoId,categoriaId,nomeProduto, descricao, preco, imagem FROM tbProduto WHERE produtoId = @produtoId", connection);
                command.Parameters.AddWithValue("@produtoId", produtoId);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    produto.Id = reader.GetInt32(0);
                    produto.CategoriaId = reader.GetInt32(1);
                    produto.Nome = reader.GetString(2);
                    produto.Descricao = reader.GetString(3);
                    produto.Preco = Convert.ToDecimal(reader.GetDecimal(4));
                    produto.Imagem = reader.GetString(5);
                    //produto.Quantidade = Convert.ToDouble(reader.GetString(5)),
                    //produto.TipoQuantidade = reader.GetString(6)
                }
                connection.Close();
                return produto;
            }
            catch (Exception ex)
            {
                return produto;
            }
        }

        public List<Imagem> ImagensProduto(int produtoId)
        {
            List<Imagem> imagens = new List<Imagem>();

            try
            {
                using var connection = new MySqlConnection(_conn);
                connection.Open();

                using var command = new MySqlCommand("SELECT imagemId, produtoId, endereco, ordem FROM tbimagem where produtoId= @produtoId ORDER BY order ASC", connection);
                command.Parameters.AddWithValue("@produtoId", produtoId);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Imagem img = new Imagem()
                    {
                        Id = reader.GetInt32(0),
                        ProdutoId = reader.GetInt32(1),
                        Endereco = reader.GetString(2),
                        Ordem = reader.GetInt32(3)
                    };
                    imagens.Add(img);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                return imagens;
            }

            return imagens;
        }

        public List<Comentario> ListaComentarios (int produtoId)
        {
            List<Comentario> comentarios = new List<Comentario>();

            try
            {
                using var connection = new MySqlConnection(_conn);
                connection.Open();

                using var command = new MySqlCommand("SELECT C.comentarioId, C.produtoId, C.usuarioId, U.nomeUsuario, C.comentario, C.dataComentario FROM tbcomentario C INNER JOIN tbUsuario U WHERE U.usuarioId = C.usuarioId AND C.status = 1 AND C.visivel = 1 AND C.produtoId = @produtoId ORDER BY dataComentario DESC", connection);
                command.Parameters.AddWithValue("@produtoId", produtoId);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Comentario comentario = new Comentario()
                    {
                        Id = reader.GetInt32(0),
                        ProdutoId = reader.GetInt32(1),
                        UsuarioId = reader.GetInt32(2),
                        NomeUsuario = reader.GetString(3),
                        Coment = reader.GetString(4),
                        DataComentario = (DateTime)reader.GetDateTime(5)
                    };
                    comentarios.Add(comentario);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                return comentarios;
            }

            return comentarios;
        }
    }
}
