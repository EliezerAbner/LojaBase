namespace LojaBase.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public DateTime DataAdicao { get; set; }
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public string? Imagem { get; set; }
        public int Quantidade { get; set; }
        public int UsuarioId { get; set; }
    }
}
