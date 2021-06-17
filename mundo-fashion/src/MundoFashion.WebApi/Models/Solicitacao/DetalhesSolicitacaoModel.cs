namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record DetalhesSolicitacaoModel
    {
        public string[] TipoEstampa { get; set; }
        public string[] TipoTecnica { get; set; }
        public string[] TipoTecnicaEstamparia { get; set; }
        public string[] TipoNicho { get; set; }
        public string[] TipoRapport { get; set; }
        public string Observacoes { get; set; }
        public string[] Imagens { get; set; }
    }
}
