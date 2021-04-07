namespace MundoFashion.Core.Dto
{
    public class EmpresaDto
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }

        public EmpresaDto(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }
    }
}
