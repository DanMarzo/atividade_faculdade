
using Microsoft.Data.SqlClient;
using Tech.Market.API.Entities;

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
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                
                return await connection.QueryFirstOrDefaultAsync<SaldoEntity>(sql, new { idConta });
            }
        }

        public async Task<SaldoEntity> InsertAsync(SaldoEntity entity)
        {
            
            string sql = $@"
                MERGE INTO Saldos AS target
                USING (VALUES (@Valor, @IdConta)) AS source (Valor, IdConta) ON target.IdConta = source.IdConta
                WHEN     MATCHED THEN UPDATE SET target.Valor = source.valor
                WHEN NOT MATCHED THEN INSERT (Valor, IdConta) 
                    VALUES (source.Valor, source.IdConta)
                OUTPUT inserted.*;
                ";

            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryFirstOrDefaultAsync<SaldoEntity>(sql, entity);
            }                
        }

        public async Task<bool> UpdateAsync(SaldoEntity entity)
        {
            string sql = @"
                UPDATE saldos
                SET valor = @Valor
                WHERE id= @Id and idConta = @IdConta
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                
                int linhas = await connection.ExecuteAsync(sql, entity);
                return linhas == 1;
            }
        }
    }
}
