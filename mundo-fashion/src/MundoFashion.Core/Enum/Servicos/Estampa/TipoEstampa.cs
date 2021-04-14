using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enum.Servicos.Estampa
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
