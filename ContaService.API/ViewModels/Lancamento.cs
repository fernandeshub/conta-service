namespace ContaService.API.ViewModels
{
    public class Lancamento
    {
        public string ContaOrigem { get; set; }
        public string ContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}