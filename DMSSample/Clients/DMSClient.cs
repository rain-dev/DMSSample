using Pinewood.DMSSample.Business.DTO;

namespace Pinewood.DMSSample.Business.Clients
{
    public class DMSClient : IDMSClient
    {
        private readonly IPartInvoiceController __Controller;

        public DMSClient(IPartInvoiceController controller)
        {
            __Controller = controller;
        }


        public Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            // return task to reduce heap allocation
            return __Controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        }

        private void ReleaseUnmanagedResources()
        {
            // remove unmanaged
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~DMSClient()
        {
            ReleaseUnmanagedResources();
        }
    }
}