namespace Tech.Market.API.Repositories
{
    public class SaldoRepository : ISaldoRepository
    {
        private readonly ConnectionStringsOptions _connection;

        public SaldoRepository(IOptionsSnapshot<ConnectionStringsOptions> connection)
        {
            this._connection = connection.Value;
        }

        public async Task<SaldoEntity?> GetByContaAsync(int idConta)
        {
            string sql = @"
                SELECT * FROM saldos s 
                WHERE s.idconta = @IdConta
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                
                return await connection.QueryFirstOrDefaultAsync<SaldoEntity>(sql, new { idConta });
            }
        }

        public async Task<SaldoEntity> InsertAsync(SaldoEntity entity)
        {
            string sql = @"
                INSERT INTO public.saldos
                    (valor, idconta)
                VALUES(@valor, @idconta)
                RETURNING *;
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                
                return await connection.QueryFirstAsync<SaldoEntity>(sql, entity);
            }
        }

        public async Task<bool> UpdateAsync(SaldoEntity entity)
        {
            string sql = @"
                UPDATE public.saldos
                SET valor = @Valor
                WHERE id= @Id and idConta = @IdConta
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                
                int linhas = await connection.ExecuteAsync(sql, entity);
                return linhas == 1;
            }
        }
    }
}
