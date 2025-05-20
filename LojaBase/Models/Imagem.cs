namespace LojaBase.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string? Endereco { get; set; }
        public int Ordem { get; set; }
    }
}
