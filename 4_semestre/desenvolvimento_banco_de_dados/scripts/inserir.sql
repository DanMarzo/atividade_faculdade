use loja;

-- Estado
INSERT INTO Estado (Nome, UF)
VALUES 
	('Rio de Janeiro', 'RJ'), 
	('São Paulo', 'SP'), 
	('Ceará', 'CE');

-- Municipio 
INSERT INTO Municipio (Estado_Id, Nome, CodIBGE)
VALUES 
	(1, 'Rio de Janeiro', 3304557),
	(2, 'São Paulo', 3550308),
	(3, 'Abaiara', 2300101);

-- Cliente
INSERT INTO Cliente (Nome, CPF, Celular, EndLogradouro, EndNumero, EndMunicipio, EndCEP, Municipio_Id)
VALUES
	('Maria', '38131812006', '67991962157', 'Avenida Atlântica',  '123', 0, '22021001', 1),
	('José',  '12345678909', '85979562177', 'Av Penha De França', '321', 0, '03606000', 2),
	('Luana', '53227274000', '19832041766', 'Rua Capitão Alex',   '987', 0, '58036020', 3);

-- Contas a receber
INSERT INTO ContaReceber (Cliente_Id, FaturaVendaId, DataConta, DataVencimento, Valor, Situacao)
VALUES 
	(1, 1, CURDATE(), DATE(DATE_ADD(CURDATE(), INTERVAL 1 MONTH)), 12.00 , '3'),
	(2, 2, CURDATE(), DATE(DATE_ADD(CURDATE(), INTERVAL 7 DAY  )), 299.88, '1'),
	(3, 3, CURDATE(), DATE(DATE_ADD(CURDATE(), INTERVAL 10 DAY )), 149.99, '2');
