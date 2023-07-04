using System.Data;
using System.Data.SqlClient;

namespace API_Project.Context
{
     public class DapperContext
     {
          private readonly IConfiguration _configuration;
          private readonly string _connectionString;

          public DapperContext(IConfiguration configuration)
          {
               this._configuration = configuration;
               this._connectionString = _configuration.GetConnectionString("SQLConnection");
          }

          public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
     }
}
