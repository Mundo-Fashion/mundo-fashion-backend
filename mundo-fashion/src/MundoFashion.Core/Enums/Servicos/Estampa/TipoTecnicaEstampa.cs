using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enums.Servicos.Estampa
{
    [Flags]
    public enum TipoTecnicaEstampa
    {
        [Description(nameof(Simplificada))]
        Simplificada = 0,
        [Description(nameof(Silk))]
        Silk = 1,
        [Description(nameof(Digital))]
        Digital = 2,        
        [Description("Sublimação")]
        Sublimacao = 4,
        [Description("Cilíndro")]
        Cilindro = 8
    }
}
