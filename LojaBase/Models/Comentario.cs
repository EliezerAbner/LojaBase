namespace LojaBase.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public string? NomeUsuario { get; set; }
        public string? Coment { get; set; }

        public DateTime? DataComentario { get; set; }
        public bool? Visivel { get; set; }
        public int Status { get; set; }
    }
}
