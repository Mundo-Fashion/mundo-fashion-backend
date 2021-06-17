namespace MundoFashion.WebApi.Models
{
    public record AutenticacaoModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}