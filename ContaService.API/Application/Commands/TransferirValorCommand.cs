using System.Runtime.Serialization;
using MediatR;

namespace ContaService.API.Application.Commands
{
    [DataContract]
    public class TransferirValorCommand : IRequest<bool>
    {
        [DataMember]
        public string ContaOrigem { get; private set; }

        [DataMember]
        public string ContaDestino { get; private set; }

        [DataMember]
        public decimal Valor { get; private set; }

        public TransferirValorCommand(string contaOrigem, string contaDestino, decimal valor)
        {
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
            Valor = valor;
        }
    }
}