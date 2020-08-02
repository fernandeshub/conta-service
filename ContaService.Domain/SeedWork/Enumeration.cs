using System.ComponentModel;

namespace ContaService.Domain.SeedWork
{
    public enum TipoOperacao 
    {
       [Description("Depósito")]
       Deposito = 1,
       [Description("Saque")]
       Saque = 2,
    }
}