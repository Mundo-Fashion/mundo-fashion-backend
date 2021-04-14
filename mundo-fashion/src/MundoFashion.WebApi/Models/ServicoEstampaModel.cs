namespace MundoFashion.WebApi.Models
{
    public record ServicoEstampaModel
    {
        public int TipoEstampa { get; set; }
        public int TipoTecnica { get; set; }
        public int TipoTecnicaEstamparia { get; set; }
        public int TipoNicho { get; set; }
        public int TipoRapport { get; set; }
    }
}
