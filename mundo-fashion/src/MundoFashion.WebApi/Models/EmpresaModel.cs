namespace MundoFashion.WebApi.Models
{
    public class EmpresaModel
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public ServicoEstampaModel Servico { get; set; }

        public EmpresaModel(string nome, string cnpj, ServicoEstampaModel servico = null)
        {
            Nome = nome;
            Cnpj = cnpj;
            Servico = servico;
        }
    }
}
