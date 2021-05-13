using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enums.Servicos.Estampa
{
    [Flags]
    public enum TipoEstampa
    {
        [Description(nameof(Localizada))]
        Localizada = 1,
        [Description(nameof(Rotativa))]
        Rotativa = 2
    }
}
