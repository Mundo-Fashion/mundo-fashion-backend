namespace MundoFashion.WebApi.Models
{
    public class EmpresaModel
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }

        public EmpresaModel(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

    }
}
