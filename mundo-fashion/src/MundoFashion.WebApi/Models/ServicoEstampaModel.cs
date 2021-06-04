using Microsoft.AspNetCore.Http;
using MundoFashion.WebApi.Models.Usuario;
using System;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models
{
    public record ServicoEstampaModel
    {
        public Guid Id { get; set; }
        public int TipoEstampa { get; set; }
        public int TipoTecnica { get; set; }
        public int TipoTecnicaEstamparia { get; set; }
        public int TipoNicho { get; set; }
        public int TipoRapport { get; set; }
        public PrestadorTomadorModel Prestador { get; set; }
        public List<IFormFile> ImagensUpload { get; set; } = new List<IFormFile>();
        public string[] Imagens { get; set; }
    }
}
