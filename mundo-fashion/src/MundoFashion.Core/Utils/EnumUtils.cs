using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MundoFashion.Core.Utils
{
    public static class EnumUtils
    {
        public static string ObterValorEmTexto<TEnum>(TEnum valor)
        {
            DescriptionAttribute description = valor.GetType().GetMember(valor.ToString())[0].GetCustomAttribute<DescriptionAttribute>();
            return description.Description;
        }

        public static string[] ObterValoresEmTextoFlagEnum<TEnum>(TEnum valor) where TEnum : Enum
        {
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));

            List<string> descricoes = new List<string>();

            foreach (TEnum value in values)
            {
                if (valor.HasFlag(value))
                    descricoes.Add(ObterValorEmTexto<TEnum>(value));
            }

            return descricoes.ToArray();
        }
    }
}
