using Pinewood.DMSSample.Business.DTO;

namespace Pinewood.DMSSample.Business
{
    public  interface IPartInvoiceController 
    {
        Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName);
    }
}
