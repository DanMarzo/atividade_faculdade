
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
            string sql = @"
                INSERT INTO saldos
                    (valor, idconta)
                VALUES(@valor, @idconta)
                RETURNING *;
            ";
            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                
                return await connection.QueryFirstAsync<SaldoEntity>(sql, entity);
            }
        }

        public async Task<IEnumerable<SaldoEntity>> InsertAsync(IEnumerable<SaldoEntity> entities)
        {
            DynamicParameters parameters = new DynamicParameters();
            
            //string values = string.Join(", ", entities.Select((e, i) =>
            //{

            //    parameters.Add($"valor{i}", e.Valor);
            //    parameters.Add($"idConta{i}", e.IdConta);
            //    return $"(@valor{i}, @idconta{i})";
            //}));

            string sql = $@"
                MERGE INTO Saldos AS target
                USING (
                    VALUES (@valor, @idconta)
                ) AS source (valor, idconta)
                    ON target.idconta = source.idconta
                WHEN MATCHED THEN
                    UPDATE SET target.valor = source.valor
                WHEN NOT MATCHED THEN
                    INSERT (valor, idconta)
                    VALUES (source.valor, source.idconta)
                OUTPUT inserted.*;
                ";

            using (DbConnection connection = new SqlConnection(this._connection.Default))
            {
                return await connection.QueryAsync<SaldoEntity>(sql, parameters);
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
