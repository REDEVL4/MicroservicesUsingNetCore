using Dapper;
using Npgsql;
using System.Data;

namespace Discount.Grpc.DataAccess
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IConfiguration _configuration;

        public GenericRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<T>> LoadData<T, U>(T? data = default(T), U? parameters = default(U), string? query = default(string))
        {
            IEnumerable<T> result;
            using IDbConnection connection = new NpgsqlConnection(_configuration.GetValue<string>("Secrets:ConnectionString"));
            result = await connection.QueryAsync<T>(query, parameters, commandType: CommandType.Text);
            return result;
        }
        public async Task InsertData<T, U>(T? data = default(T), U? parameters = default(U), string? query = default(String))
        {
            using IDbConnection connection = new NpgsqlConnection(_configuration.GetValue<string>("Secrets:ConnectionString"));
            await connection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
        }

    }
}
