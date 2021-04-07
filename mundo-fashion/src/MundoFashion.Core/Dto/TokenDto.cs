namespace MundoFashion.Core.Dto
{
    public class TokenDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; private set; }

        public TokenDto(UsuarioDto usuario, string token)
        {
            Usuario = usuario;
            Token = token;
        }
    }
}
