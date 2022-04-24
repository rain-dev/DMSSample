using Pinewood.DMSSample.Data.Models;

namespace Pinewood.DMSSample.Data
{
    public interface ICustomerRepositoryDB : IReadRepository<Customer, int>
    {
        Task<Customer> GetByName(string name);
    }
}
