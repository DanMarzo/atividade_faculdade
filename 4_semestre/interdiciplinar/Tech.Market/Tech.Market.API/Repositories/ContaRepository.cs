
using System.Linq;

namespace Tech.Market.API.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ConnectionStringsOptions _connection;

        public ContaRepository(IOptionsSnapshot<ConnectionStringsOptions> connection)
        {
            this._connection = connection.Value;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            string sql = @"
                SELECT EXISTS (
                	SELECT 1 FROM Contas WHERE Id = @Id
                );
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                
                return await connection.QueryFirstOrDefaultAsync<bool>(sql, new { id });
            }
        }

        public async Task<IEnumerable<ContaEntity>> GetAsync(IEnumerable<int> ids)
        {
            if(!ids.Any())
                return Enumerable.Empty<ContaEntity>();
            DynamicParameters parameters = new();
            string sql = @$"
                SELECT * FROM Contas 
                WHERE Id IN ({string.Join(",", ids.Select((v, i) =>
                {
                    parameters.Add($"id{i}", v);
                    return $"@id{i}";
                }))})
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<ContaEntity>(sql, parameters);
            }
        }
    }
}
