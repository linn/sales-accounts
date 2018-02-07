namespace Linn.SalesAccounts.Domain.Dispatchers
{
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;

    public interface ISalesAccountUpdatedDispatcher
    {
        void SendSalesAccountUpdated(SalesAccountMessage message);
    }
}
