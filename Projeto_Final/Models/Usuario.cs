namespace Projeto_Final.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public bool Status { get; set; }
    }
}