using Tech.Market.Core.Entities;

namespace Tech.Market.API.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ConnectionStringsOptions _connection;
        public TransacaoRepository(IOptionsSnapshot<ConnectionStringsOptions> connection)
        {
            this._connection = connection.Value;
        }

        public async Task<IEnumerable<TransacaoEntity>> GetAsync(int? idConta = null)
        {
            StringBuilder sql = new StringBuilder(@"
                SELECT transacoes.* FROM transacoes
                INNER JOIN contas ON contas.id  = transacoes.idconta 
                WHERE 1 = 1 
            ");
            if (idConta.HasValue)
                sql.AppendLine(" AND contas.id = @idConta ");

            using (DbConnection connection = new NpgsqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<TransacaoEntity>(sql.ToString(), param: new
                {
                    idConta
                });
            }
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
