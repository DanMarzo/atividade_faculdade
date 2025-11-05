using Tech.Market.API.Entities;

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

            using (DbConnection connection = new SqlConnection(this._connection.Default))
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
                INSERT INTO TECH_MARKET_DB.dbo.Transacoes
                    (
                        IdExterno, 
                        CriadoEm, 
                        AtualizadoEm, 
                        CodigoOperacao, 
                        IdConta, 
                        IdContaDestino, 
                        Valor
                    )
                OUTPUT INSERTED.*
                VALUES
                    (
                        @IdExterno, 
                        @CriadoEm, 
                        @AtualizadoEm, 
                        @CodigoOperacao, 
                        @IdConta, 
                        @IdContaDestino, 
                        @Valor
                    );
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                entity = await connection.QueryFirstOrDefaultAsync<TransacaoEntity>(sql, entity);
                //var rr = await connection.ExecuteAsync(sql, parametros);
                return entity;
            }
        }
    }
}