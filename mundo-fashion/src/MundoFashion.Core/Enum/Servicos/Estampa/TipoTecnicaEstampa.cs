using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enum.Servicos.Estampa
{
    [Flags]
    public enum TipoTecnicaEstampa
    {
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
