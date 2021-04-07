using System;

namespace MundoFashion.Core.Dto
{
    public class UsuarioDto
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
