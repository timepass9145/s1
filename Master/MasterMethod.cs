using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIBSAPI.Data;
using System.Data;

namespace SIBSAPI.Master
{
    public class MasterMethod
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;


        // ✅ Constructor to Inject DbContext
        public MasterMethod(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Database connection string is missing or not configured.");
            }

        }

        public async Task<DataTable> GetLoginTableData(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("SQL Query cannot be null or empty.", nameof(query));
            }

            using (var connection = new SqlConnection(_connectionString)) // Use Dapper-friendly connection
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }













        //public async Task<IEnumerable<Dictionary<string, object>>> ExecuteQueryAsync(string query)
        //{
        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        connection.ConnectionString = _context.Database.GetConnectionString();
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                var result = new List<Dictionary<string, object>>();
        //                while (await reader.ReadAsync())
        //                {
        //                    var dict = new Dictionary<string, object>();
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        dict[reader.GetName(i)] = reader[i] == DBNull.Value ? null : reader[i];
        //                    }
        //                    result.Add(dict);
        //                }
        //                return result;
        //            }
        //        }
        //    }
        //}
        //✅ Convert DataTable to List of Dictionary for JSON serialization
        public List<Dictionary<string, object>> ConvertDataTableToJson(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        // ✅ Fetch data from the database using raw SQL
        //public async Task<DataTable> GetLoginTableData(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        throw new ArgumentException("SQL Query cannot be null or empty.", nameof(query));
        //    }

        //    if (_context == null)
        //    {
        //        throw new NullReferenceException("ApplicationDbContext (_context) is null. Ensure dependency injection is working.");
        //    }

        //    using (var connection = _context.Database.GetDbConnection()) // Get database connection
        //    {
        //        if (connection == null)
        //        {
        //            throw new NullReferenceException("Database connection is null. Ensure your database is configured correctly.");
        //        }

        //        await connection.OpenAsync(); // Open connection
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = query; // Set the SQL query

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                var dataTable = new DataTable();
        //                dataTable.Load(reader); // Load SQL data into DataTable
        //                await connection.CloseAsync(); // Open connection
        //                return dataTable;
        //            }
        //        }
        //    }
        //}

        //SQL Multiple query Exicution 
        public async Task<Dictionary<string, List<Dictionary<string, object>>>> TExecuteMultipleQueriesAsync(string query, Dictionary<string, object>? parameters = null, List<string>? resultSetNames = null)
        {
            var results = new Dictionary<string, List<Dictionary<string, object>>> ();        
            int resultSetIndex = 0;


            using (var connection = _context.Database.GetDbConnection())
            {
                connection.ConnectionString = _context.Database.GetConnectionString();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(new SqlParameter(param.Key, param.Value ?? DBNull.Value));
                        }
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                       while (!reader.IsClosed) // 🔥 Instead of using NextResultAsync(), we check if reader is open
                       {
                            if (!reader.HasRows) break; // ✅ If no rows, stop processing

                            var dataTable = new DataTable();
                            dataTable.Load(reader); // 🚀 Load current result set

                            var resultList = dataTable.AsEnumerable()
                                .Select(row => dataTable.Columns.Cast<DataColumn>()
                                    .ToDictionary(col => col.ColumnName, col => row[col]))
                                .ToList();

                            string resultSetName = (resultSetNames != null && resultSetIndex < resultSetNames.Count)
                                ? resultSetNames[resultSetIndex]
                                : $"ResultSet_{resultSetIndex + 1}";

                            results[resultSetName] = resultList;

                            resultSetIndex++;

                            if (reader.IsClosed) break; // 🚀 Ensure the reader is still open before continuing
                       }
                        
                    }
                }
                await connection.CloseAsync();
            }

            return results;
        }





    }
}
