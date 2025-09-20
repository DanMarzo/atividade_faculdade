CREATE VIEW view_contas_pendentes_pagamento AS
SELECT 
	cr.Id  			  AS IdContaReceber,
	c.Nome 			  AS NomeCliente,
	c.cpf             AS CPFCliente,
	cr.DataVencimento AS DataVencimentoConta,
	cr.Valor          AS ValorConta
FROM loja.ContaReceber cr
INNER JOIN loja.Cliente c ON c.Id = cr.Cliente_Id
WHERE cr.Situacao = '1';