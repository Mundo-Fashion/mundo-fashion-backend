using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models.Solicitacao
{
    public record NovoDetalhesSolicitacaoModel
    {
        public int TipoEstampa { get; set; }
        public int TipoTecnica { get; set; }
        public int TipoTecnicaEstamparia { get; set; }
        public int TipoNicho { get; set; }
        public int TipoRapport { get; set; }
        public string Observacoes { get; set; }
        public List<IFormFile> ImagensUpload { get; set; } = new List<IFormFile>();
    }
}
