using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enums.Servicos.Estampa
{
    [Flags]
    public enum TipoEstampa
    {
        [Description(nameof(Simplificada))]
        Simplificada = 0,
        [Description(nameof(Localizada))]
        Localizada = 1,
        [Description(nameof(Rotativa))]
        Rotativa = 2
    }
}
