using MundoFashion.Core.Enums.Servicos.Estampa;

namespace MundoFashion.Core.Dto
{
    public record ServicoEstampaDto
    {
        public TipoEstampa TipoEstampa { get; private set; }
        public TipoTecnicaEstampa Tecnica { get; private set; }
        public TipoTecnicaEstamparia TecnicaEstamparia { get; private set; }
        public TipoNicho Nicho { get; private set; }
        public TipoRapport TipoRapport { get; private set; }

        public ServicoEstampaDto(TipoEstampa tipoEstampa, TipoTecnicaEstampa tecnica, TipoTecnicaEstamparia tecnicaEstamparia, TipoNicho nicho, TipoRapport tipoRapport)
        {
            TipoEstampa = tipoEstampa;
            Tecnica = tecnica;
            TecnicaEstamparia = tecnicaEstamparia;
            Nicho = nicho;
            TipoRapport = tipoRapport;
        }
    }
}
