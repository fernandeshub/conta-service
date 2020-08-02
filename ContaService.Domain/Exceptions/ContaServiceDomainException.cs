using System;
using System.Collections.Generic;
using System.Text;

namespace ContaService.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class ContaServiceDomainException : Exception
    {
        public ContaServiceDomainException()
        { }

        public ContaServiceDomainException(string message)
            : base(message)
        { }

        public ContaServiceDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}