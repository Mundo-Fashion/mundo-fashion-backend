using Microsoft.AspNetCore.Http;

namespace MundoFashion.WebApi.Models.Usuario
{
    public record UsuarioAtualizadoModel
    {
        public IFormFile ImagemAvatar { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string DescricaoPessoal { get; set; }
    }
}
