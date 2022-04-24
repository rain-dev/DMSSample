using Pinewood.DMSSample.Data.Models;
using System.Data;
using System.Data.SqlClient;

namespace Pinewood.DMSSample.Data
{
    public interface IReadRepository<TModel, TId>
        where TId : struct
        where TModel : ModelBase<TId>
    {
        Task<TModel> Get(string script, CommandType commandType, params SqlParameter[] parameters);
    }

    public interface IWriteRepository
    {
        Task Update(string script, CommandType commandType, params SqlParameter[] parameters);
    }

    public interface IRepository<TModel, TId> : IReadRepository<TModel, TId>, IWriteRepository
        where TId : struct
        where TModel : ModelBase<TId>
    {

    }
}
