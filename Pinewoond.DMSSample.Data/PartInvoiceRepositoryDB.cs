using Pinewood.DMSSample.Data.Models;
using System.Data;
using System.Data.SqlClient;

namespace Pinewood.DMSSample.Data
{
    public class PartInvoiceRepositoryDB : IPartInvoiceRepositoryDB
    {
        private readonly SqlCommandFactory _sqlCommandFactory;

        public PartInvoiceRepositoryDB(
            SqlCommandFactory sqlCommandFactory)
        {
            _sqlCommandFactory = sqlCommandFactory;
        }
        public Task AddInvoice(PartInvoice invoice)
        {
            return Update("PMS_AddPartInvoice", CommandType.StoredProcedure, new SqlParameter?[]
            {
                new SqlParameter("@StockCode", SqlDbType.VarChar, 50) { Value = invoice.StockCode },
                new SqlParameter("@Quantity", SqlDbType.Int) { Value = invoice.Quantity },
                new SqlParameter("@CustomerID", SqlDbType.Int) { Value = invoice.CustomerID }
            });
        }

        public Task Update(string script, CommandType commandType, params SqlParameter?[] parameters)
        {
            var command = _sqlCommandFactory.GetCommand(o => o.CommandType == CommandType.StoredProcedure);
            try
            {
                if (command is null) throw new NullReferenceException("Command is not available");

                command.CommandText = script;
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                command.ExecuteNonQuery();

                return Task.CompletedTask;
            }
            finally
            {
                if (command is not null)
                    command?.Connection.Close();
            }
        }
    }
}
