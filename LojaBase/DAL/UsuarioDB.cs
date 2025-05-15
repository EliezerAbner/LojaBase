using LojaBase.Models;
using LojaBase.Services;

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

        public void /*List<Usuario>*/ ListaUsuarios() 
        {
            //listar usuarios
        }

        public void NovoUsuario(Usuario usuario, Endereco end) 
        {

        }
    }
}


//using (SqlConnection conn = new SqlConnection(_connString))
//{
//    conn.Open();
//    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id", conn);
//    cmd.Parameters.AddWithValue("@id", id);

//    using (SqlDataReader reader = cmd.ExecuteReader())
//    {
//        if (reader.Read())
//        {
//            return new User
//            {
//                Id = (int)reader["Id"],
//                Name = reader["Name"].ToString()
//            };
//        }
//    }
//}