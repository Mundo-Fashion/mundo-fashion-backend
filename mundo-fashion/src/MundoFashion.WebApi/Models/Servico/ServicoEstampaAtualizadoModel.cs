using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models.Servico
{
    public record ServicoEstampaAtualizadoModel
    {
        public int TipoEstampa { get; set; }
        public int Tecnica { get; set; }
        public int TecnicaEstamparia { get; set; }
        public int Nicho { get; set; }
        public int TipoRapport { get; set; }
        public string Descricao { get; set; }
        public List<IFormFile> ImagensUpload { get; set; } = new List<IFormFile>();
    }
}
