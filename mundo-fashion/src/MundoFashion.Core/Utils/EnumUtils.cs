using System;
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
    }
}
