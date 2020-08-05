using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ContaService.Domain.SeedWork
{
    public enum TipoOperacao
    {
        [Description("Crédito")]
        Credito = 1,
        [Description("Débito")]
        Debito = 2,
    }
    public static class Enumeration
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }



}