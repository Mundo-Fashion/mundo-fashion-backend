using System.ComponentModel;

namespace MundoFashion.Core.Enums.Solicitacao
{
    public enum StatusSolicitacao
    {
        [Description("Solicitado")]
        Solicitado,
        [Description("Em Negociação")]
        EmNegociacao,
        [Description("Analisando Proposta")]
        AnalisandoProposta,
        [Description("Cancelada")]
        Cancelada,
        [Description("Negociado")]
        Negociado,
        [Description("Pago")]
        Pago,
        [Description("Entregue")]
        Entregue,
        [Description("Finalizado")]
        Finalizado
    }
}
