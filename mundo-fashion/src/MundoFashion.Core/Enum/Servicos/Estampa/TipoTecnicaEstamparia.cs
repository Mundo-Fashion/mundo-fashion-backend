using System;
using System.ComponentModel;

namespace MundoFashion.Core.Enum.Servicos.Estampa
{
    [Flags]
    public enum TipoTecnicaEstamparia
    {
        [Description(nameof(Gel))]
        Gel = 1,
        [Description(nameof(Relevo))]
        Relevo = 2,
        [Description(nameof(Puff))]
        Puff = 4,
        [Description(nameof(Foil))]
        Foil = 8,
        [Description(nameof(Gliter))]
        Gliter = 16,
        [Description("Bilha no escuro")]
        BilhaEscuro = 32
    }
}
