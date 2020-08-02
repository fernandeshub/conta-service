using System.ComponentModel;

namespace ContaService.Domain.SeedWork
{
    public enum TipoOperacao 
    {
       [Description("Crédito")]
       Credito = 1,
       [Description("Débito")]
       Debito = 2,
    }
}