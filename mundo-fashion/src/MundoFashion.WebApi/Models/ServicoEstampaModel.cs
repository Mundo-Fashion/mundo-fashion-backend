using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models
{
    public record ServicoEstampaModel
    {
        public int TipoEstampa { get; set; }
        public int TipoTecnica { get; set; }
        public int TipoTecnicaEstamparia { get; set; }
        public int TipoNicho { get; set; }
        public int TipoRapport { get; set; }
        public List<IFormFile> ImagensUpload { get; set; }
        public string[] Imagens { get; set; }
    }
}
