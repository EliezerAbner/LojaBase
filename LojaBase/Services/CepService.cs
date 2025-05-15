using LojaBase.Models;
using System.Text.Json;

namespace LojaBase.Services
{
    //Responsável por realizar a busca do CEP, para auto preenhimento no cadastro

    public class CepService
    {
        private readonly HttpClient? _httpClient;

        public CepService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Endereco> ObterCep (string cep)
        {
            Endereco? resultado = new Endereco(); 
            
            try
            {
                var url = $"https://viacep.com.br/ws/{cep}/json/";
                var resposta = await _httpClient.GetAsync(url);

                var json = await resposta.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                resultado = JsonSerializer.Deserialize<Endereco>(json, options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return resultado;
        }
    }
}
