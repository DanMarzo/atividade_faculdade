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
    }
}
