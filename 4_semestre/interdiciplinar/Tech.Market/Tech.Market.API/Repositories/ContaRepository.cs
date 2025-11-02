namespace Tech.Market.API.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ConnectionStringsOptions _connection;

        public ContaRepository(IOptionsSnapshot<ConnectionStringsOptions> connection)
        {
            this._connection = connection.Value;
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            string sql = @"
                SELECT EXISTS (
                	SELECT 1 FROM Contas WHERE IdExterno = @Id
                );
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {

                return await connection.QueryFirstOrDefaultAsync<bool>(sql, new { id });
            }
        }

        public async Task<IEnumerable<ContaEntity>> GetAsync(IEnumerable<int> ids)
        {
            if (!ids.Any())
                return Enumerable.Empty<ContaEntity>();
            //DynamicParameters parameters = new();
            //string sql = @$"
            //    SELECT * FROM Contas 
            //    WHERE Id IN ({string.Join(",", ids.Select((v, i) =>
            //    {
            //        parameters.Add($"id{i}", v);
            //        return $"@id{i}";
            //    }))})
            //";
            string sql = @$"
                SELECT * FROM Contas 
                WHERE Id IN @ids
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<ContaEntity>(sql, new { ids });
            }
        }

        public async Task<IEnumerable<ContaEntity>> GetAsync()
        {
            string sql = "SELECT * FROM Contas";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<ContaEntity>(sql);
            }
        }

        public async Task<ContaEntity?> GetAsync(Guid id)
        {
            string sql = "SELECT * FROM Contas WHERE IdExterno = @id";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryFirstOrDefaultAsync<ContaEntity>(sql, new { id });
            }
        }

        public async Task<IEnumerable<ContaEntity>> GetAsync(params Guid[] ids)
        {
            if (!ids.Any())
                return Enumerable.Empty<ContaEntity>();
            //DynamicParameters parameters = new();
            //string sql = @$"
            //    SELECT * FROM Contas 
            //    WHERE IdExterno IN ({string.Join(",", ids.Select((v, i) =>
            //{
            //    parameters.Add($"id{i}", v);
            //    return $"@id{i}";
            //}))})
            //";
            string sql = @$"
                SELECT * FROM Contas 
                WHERE IdExterno IN @ids
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<ContaEntity>(sql, new { ids });
            }
        }

        public async Task<ContaEntity> InsertAsync(ContaEntity entity)
        {
            string sql = @$"
                INSERT INTO [dbo].[Contas]
                    (
                         IdExterno
                        ,CriadoEm
                        ,AtualizadoEm
                        ,Nome
                        ,CPF
                        ,Celular
                        ,Telefone
                        ,NascEm
                    )
                OUTPUT INSERTED.*
                VALUES
                    (
                         @IdExterno
                        ,@CriadoEm
                        ,@AtualizadoEm
                        ,@Nome
                        ,@CPF
                        ,@Celular
                        ,@Telefone
                        ,@NascEm
                    );

            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryFirstAsync<ContaEntity>(sql, entity);
            }
        }
    }
}
