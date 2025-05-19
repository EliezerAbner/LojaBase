using System.Text.Json;
using System;
using LojaBase.Models;

namespace LojaBase.Services
{
    //Responsável por ler o Json contendo a string de conexão

    public class DBService
    {
        private readonly string _caminhoRoot;
        private readonly string _caminhoArquivo;      

        public DBService() 
        {
           _caminhoRoot = Directory.GetCurrentDirectory();
           _caminhoArquivo = Path.Combine(_caminhoRoot, "Conn", "conn.json");
        }

        public string LoadJson()
        {
            string jsonString = File.ReadAllText(_caminhoArquivo);
            
            StringConexao? con = new StringConexao();
            con = JsonSerializer.Deserialize<StringConexao>(jsonString);

            jsonString = con.conn;
            
            return jsonString;

            //    Person person = JsonSerializer.Deserialize<Person>(jsonString);

        }

    }
}
