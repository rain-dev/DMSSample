using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Business.DTO;
using Pinewood.DMSSample.Data;
using Pinewood.DMSSample.Data.Models;

namespace Pinewood.DMSSample.Business
{
    public class PartInvoiceController : IPartInvoiceController
    {
        private readonly ICustomerRepositoryDB _customerRepositoryDB;
        private readonly IPartAvailabilityClient _partAvailabilityClient;
        private readonly IPartInvoiceRepositoryDB _partInvoiceRepositoryDB;

        public PartInvoiceController(ICustomerRepositoryDB customerRepositoryDB, IPartAvailabilityClient partAvailabilityClient, IPartInvoiceRepositoryDB partInvoiceRepositoryDB)
        {
            _customerRepositoryDB = customerRepositoryDB;
            _partAvailabilityClient = partAvailabilityClient;
            _partInvoiceRepositoryDB = partInvoiceRepositoryDB;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            if (string.IsNullOrEmpty(stockCode) || quantity <=0)
                return new CreatePartInvoiceResult(false);

            var _Customer = await _customerRepositoryDB.GetByName(customerName);
            int _CustomerID = _Customer?.Id ?? 0;
            if (_CustomerID <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }
            int _Availability = await _partAvailabilityClient.GetAvailability(stockCode);
            if (_Availability <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var _partInvoice = new PartInvoice(
                stockCode: stockCode,
                quantity: quantity,
                customerID: _CustomerID
            );
            try
            {
                await _partInvoiceRepositoryDB.AddInvoice(_partInvoice);
            }
            catch
            {
                return new CreatePartInvoiceResult(false);
            }

            return new CreatePartInvoiceResult(true);
        }
    }
}
