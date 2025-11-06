CREATE OR ALTER PROCEDURE CalcSaldoConta(@IdConta INT, @Inicio DATETIME2, @Fim DATETIME2)
AS
BEGIN
	DECLARE @SaldoConta INT, @TotalTransacoes INT;	

	SELECT @SaldoConta = valor FROM Saldos 
	WHERE IdConta = @IdConta;
	 
	SELECT 
		TOP (10) Transacoes.*, 
		@SaldoConta AS SaldoAtual 
	INTO #TransacoesTemp
	FROM Transacoes
		WHERE 
			IdConta = @IdConta
			AND CriadoEm >= @Inicio 
			AND CriadoEm <= @Fim
		ORDER BY CriadoEm DESC;
	
	SELECT @TotalTransacoes = SUM(Valor) FROM #TransacoesTemp;

	SELECT t.*, @TotalTransacoes AS TotalTransacoes FROM #TransacoesTemp t;
	DROP TABLE #TransacoesTemp;

END;

EXEC CalcSaldoConta @IdConta = 2, @Inicio = '2025-11-01 00:00:00', @Fim = '2025-11-11 23:59:59';
