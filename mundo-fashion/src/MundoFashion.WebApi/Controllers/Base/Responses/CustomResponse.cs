using System.Collections.Generic;
using System.Net;

namespace MundoFashion.WebApi.Controllers.Base.Responses
{
    public class CustomResponse
    {
        private readonly List<string> _erros;
        public HttpStatusCode StatusCode { get; set; }
        public IReadOnlyCollection<string> Erros => _erros;
        public object Data { get; private set; }
        public CustomResponse(HttpStatusCode statusCode, object data)
        {
            StatusCode = statusCode;
            Data = data;
            _erros = new List<string>();
        }

        public CustomResponse AdicionarErro(string erro)
        {
            _erros.Add(erro);
            return this;
        }

        public CustomResponse AdicionarErros(IEnumerable<string> erros)
        {
            _erros.AddRange(erros);
            return this;
        }

    }
}
