using MundoFashion.Core.Enum.Servicos.Estampa;

namespace MundoFashion.Domain.Servicos
{
    public class ServicoEstampa : Servico
    {
        public TipoEstampa TipoEstampa { get; private set; }
        public TipoTecnicaEstampa Tecnica { get; private set; }
        public TipoTecnicaEstamparia TecnicaEstamparia { get; private set; }
        public TipoNicho Nicho { get; private set; }
        public TipoRapport TipoRapport { get; private set; }

        public ServicoEstampa() { }

        public ServicoEstampa(
            TipoEstampa tipoEstampa,
            TipoTecnicaEstampa tecnica,
            TipoTecnicaEstamparia tecnicaEstamparia,
            TipoNicho nicho,
            TipoRapport tipoRapport) 
        {
            TipoEstampa = tipoEstampa;
            Tecnica = tecnica;
            TecnicaEstamparia = tecnicaEstamparia;
            Nicho = nicho;
            TipoRapport = tipoRapport;
        }

        public void AtualizarTipoEstampa(TipoEstampa tipoEstampa)
        {
            TipoEstampa = tipoEstampa;
        }

        public void AtualizarTipoTecnicaEstampa(TipoTecnicaEstampa tecnica)
        {
            Tecnica = tecnica;
        }

        public void AtualizarTipoTecnicaEstamparia(TipoTecnicaEstamparia tipoTecnicaEstamparia)
        {
            TecnicaEstamparia = tipoTecnicaEstamparia;
        }

        public void AtualizarTipoNicho(TipoNicho tipoNicho)
        {
            Nicho = tipoNicho;
        }

        public void AtualizarTipoRapport(TipoRapport tipoRapport)
        {
            TipoRapport = tipoRapport;
        }

    }
}
