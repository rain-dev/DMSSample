using Pinewood.DMSSample.Data.Models;

namespace Pinewood.DMSSample.Data
{
    public interface IPartInvoiceRepositoryDB : IWriteRepository
    {
        Task AddInvoice(PartInvoice invoice);
    }
}
