namespace Tech.Market.API.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ConnectionStringsOptions _connection;
        public TransacaoRepository(IOptionsSnapshot<ConnectionStringsOptions> connection)
        {
            this._connection = connection.Value;
        }
        public async Task<TransacaoEntity> InsertAsync(TransacaoEntity entity)
        {
            string sql = @"
                INSERT INTO public.transacoes
                (CodigoOperacao, idconta, idcontaDestino, valor)
                VALUES
                (@CodigoOperacao, @IdConta, @idcontaDestino, @valor)
                RETURNING *;
            ";
            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                return await connection.QueryFirstAsync<TransacaoEntity>(sql, param: entity);
            }
        }
    }
}
