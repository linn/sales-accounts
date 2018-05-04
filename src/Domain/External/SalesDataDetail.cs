namespace Linn.SalesAccounts.Domain.External
{
    public class SalesDataDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Id2 { get; set; }

        public string Name2 { get; set; }

        public decimal Quantity { get; set; }

        public decimal CurrencyValue { get; set; }

        public decimal BaseValue { get; set; }

        public decimal BaseMargin { get; set; }

        public decimal CalculatedQuantity { get; set; }

        public string TextDisplay { get; set; }

        public string CurrencyCode { get; set; }
    }
}
