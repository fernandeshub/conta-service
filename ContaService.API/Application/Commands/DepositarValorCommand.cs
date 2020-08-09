using System.Runtime.Serialization;
using MediatR;

namespace ContaService.API.Application.Commands
{
    [DataContract]
    public class DepositarValorCommand : IRequest<bool>
    {
        [DataMember]
        public string ContaNumero { get; private set; }

        [DataMember]
        public decimal Valor { get; private set; }

        public DepositarValorCommand(string contaNumero, decimal valor)
        {
            ContaNumero = contaNumero;
            Valor = valor;
        }
    }
}