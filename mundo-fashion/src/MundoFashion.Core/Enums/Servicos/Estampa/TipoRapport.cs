using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enums.Servicos.Estampa
{
    [Flags]
    public enum TipoRapport
    {
        [Description(nameof(Simplificada))]
        Simplificada = 0,
        [Description("16x16")]
        Rapport16x = 1,
        [Description("32x32")]
        Rapport32x = 2,
        [Description("64x64")]
        Rapport64x = 4,
        [Description("21.33x21.33")]
        Rapport2133x = 8

    }
}
