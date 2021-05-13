using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MundoFashion.WebApi.Models
{
    public record SolicitacaoModel
    {
        public string Status { get; set; }
        public DetalhesSolicitacaoModel Detalhes { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeEmpresa { get; set; }
        public Guid? UsuarioId { get; set; }
        public Guid? EmpresaId { get; set; }
        public Guid? ServicoId { get; set; }
        public bool IsEmpresa { get; set; }        
    }
}
