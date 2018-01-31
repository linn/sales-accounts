﻿namespace Linn.SalesAccounts.Proxy
{
    using System.Collections.Generic;

    public static class DefaultHeaders
    {
        public static IDictionary<string, string[]> JsonHeaders()
        {
            return new Dictionary<string, string[]> { { "Accept", new[] { "application/json" } }, { "Content-Type", new[] { "application/json" } } };
        }
    }
}
