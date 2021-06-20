using System;

namespace MundoFashion.WebApi.Models.Usuario
{
    public record PrestadorTomadorModel
    {
        public Guid Id { get; set; }
        public string AvatarLink { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }                
        public string Cpf { get; set; }
        public string DescricaoPessoal { get; set; }
    }
}