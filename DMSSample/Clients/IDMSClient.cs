using Pinewood.DMSSample.Business.DTO;

namespace Pinewood.DMSSample.Business.Clients
{
    public interface IDMSClient : IDisposable
    {
        Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName);
    }
}
