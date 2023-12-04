namespace Projeto_Final.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public float Preco { get; set; }
        public bool Status { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public int IdUsuarioUpdate { get; set; }
    }
}