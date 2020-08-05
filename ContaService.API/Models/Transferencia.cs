namespace ContaService.API.Models
{
    public class Transferencia
    {
        public string ContaOrigem { get; set; }
        public string ContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}