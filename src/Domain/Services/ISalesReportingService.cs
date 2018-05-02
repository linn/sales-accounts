namespace Linn.SalesAccounts.Domain.Services
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain.External;

    public interface ISalesReportingService
    {
        IEnumerable<SalesDataDetail> GetSalesByAccount(string financialYear);
    }
}
