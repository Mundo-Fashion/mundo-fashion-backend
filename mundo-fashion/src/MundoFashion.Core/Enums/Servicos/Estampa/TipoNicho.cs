using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enums.Servicos.Estampa
{
    [Flags]
    public enum TipoNicho
    {
        [Description(nameof(Simplificada))]
        Simplificada = 0,
        [Description(nameof(Feminino))]
        Feminino = 1,
        [Description(nameof(Masculino))]
        Masculino = 2,
        [Description(nameof(Infantil))]
        Infantil = 4,
        [Description(nameof(Pijama))]
        Pijama = 8
    }
}
