using Pinewood.DMSSample.Data.Models;
using System.Data;
using System.Data.SqlClient;

namespace Pinewood.DMSSample.Data
{
    public class CustomerRepositoryDB : ICustomerRepositoryDB
    {
        private readonly SqlCommandFactory _sqlCommandFactory;

        public CustomerRepositoryDB(
            SqlCommandFactory sqlCommandFactory)
        {
            _sqlCommandFactory = sqlCommandFactory;
        }
        public async Task<Customer> Get(string script, CommandType commandType, params SqlParameter?[] parameters)
        {
            var command = _sqlCommandFactory.GetCommand(o => o.CommandType == CommandType.StoredProcedure);
            try
            {
                Customer customer = default;
                if (command is null) throw new NullReferenceException("Command is not available");

                command.CommandText = script;
                foreach(var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    customer = new(
                        id: (int)reader["CustomerID"],
                        name: (string)reader["Name"],
                        address: (string)reader["Address"]
                    );
                }

                return customer;
            }
            finally
            {
                if (command is not null)
                    command?.Connection.Close();

            }

        }

        public Task<Customer> GetByName(string name)
        {
            return Get("CRM_GetCustomerByName", CommandType.StoredProcedure, new SqlParameter?[]
            {
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = name },
            });
        }
    }
}
