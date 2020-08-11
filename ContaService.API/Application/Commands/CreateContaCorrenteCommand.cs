using System.Runtime.Serialization;
using MediatR;

namespace ContaService.API.Application.Commands
{
    [DataContract]
    public class CreateContaCorrenteCommand : IRequest<bool>
    {
        [DataMember]
        public string Numero { get; private set; }

        public CreateContaCorrenteCommand(string numero)
        {
            Numero = numero;
        }
    }
}