using MundoFashion.WebApi.Models.Usuario;
using System;

namespace MundoFashion.WebApi.Models
{
    public record ServicoEstampaModel
    {
        public Guid Id { get; set; }
        public string[] TipoEstampa { get; set; }
        public string[] TipoTecnica { get; set; }
        public string[] TipoTecnicaEstamparia { get; set; }
        public string[] TipoNicho { get; set; }
        public string[] TipoRapport { get; set; }
        public string Descricao { get; set; }
        public PrestadorTomadorModel Prestador { get; set; }
        public string[] Imagens { get; set; }
    }
}
