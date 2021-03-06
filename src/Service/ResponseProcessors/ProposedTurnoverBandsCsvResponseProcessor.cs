﻿namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain.Models;

    public class ProposedTurnoverBandsCsvResponseProcessor : CsvResponseProcessor<IEnumerable<ProposedTurnoverBandModel>>
    {
        public ProposedTurnoverBandsCsvResponseProcessor() : base(null, "sales.account-proposed-turnover-bands", 1)
        {
        }
    }
}