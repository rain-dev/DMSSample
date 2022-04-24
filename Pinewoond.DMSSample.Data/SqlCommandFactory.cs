using System.Data.SqlClient;

namespace Pinewood.DMSSample.Data
{
    public class SqlCommandFactory
    {
        private readonly IEnumerable<SqlCommand> _sqlCommand;
        public SqlCommandFactory(IEnumerable<SqlCommand> sqlCommand)
        {
            _sqlCommand = sqlCommand;
        }

        public SqlCommand? GetCommand(Func<SqlCommand, bool> expr )
        {
            return _sqlCommand.FirstOrDefault( expr );
        }
    }
}
