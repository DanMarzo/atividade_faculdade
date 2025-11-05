CREATE DATABASE TECH_MARKET_DB;
go

USE TECH_MARKET_DB;

CREATE TABLE Contas
(
    Id              INT                 PRIMARY KEY IDENTITY(1,1),
    IdExterno       UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID(),
    CriadoEm        DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    AtualizadoEm    DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Nome            VARCHAR(150)        NOT NULL,
    CPF             VARCHAR(20)         NOT NULL,
    Celular         VARCHAR(20)         NOT NULL, -- tamanho a mais para caso o mesmo mude o tipo (ex: CNPJ que ira passar a ter letras)
    Telefone        VARCHAR(20)         NOT NULL,
    NascEm          DATE                NOT NULL
);

CREATE UNIQUE INDEX idx_unique_contas_id_externo ON Contas (IdExterno);
CREATE UNIQUE INDEX idx_unique_contas_cpf        ON Contas (cpf);

INSERT INTO contas(nome, cpf, Celular, Telefone, NascEm)
VALUES
    ('Cleiton', '12345678909', '11999999999', '1188888888', '2000-12-01'), 
    ('Paula', '71875805095', '11999999999', '1188888888', '2000-01-01');

CREATE TABLE Saldos
(
    Id              INT                 PRIMARY KEY IDENTITY(1,1),
    IdExterno       UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID(),
    CriadoEm        DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    AtualizadoEm    DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Valor           DECIMAL NOT NULL,
    IdConta         INT NOT NULL
);

ALTER TABLE Saldos  
    ADD CONSTRAINT FK_CONTA_TO_SALDOS
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

CREATE UNIQUE INDEX idx_unique_saldos_id_conta   ON Saldos (IdConta);
CREATE UNIQUE INDEX idx_unique_saldos_id_externo ON Saldos (IdExterno);


INSERT INTO Saldos(Valor, IdConta)
VALUES (10.1, 1), (13.13, 2);


CREATE TABLE Transacoes
(
    Id              INT                 PRIMARY KEY IDENTITY(1,1),
    IdExterno       UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID(),
    CriadoEm        DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    AtualizadoEm    DATETIME2           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CodigoOperacao  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID(),
    IdConta         INT                 NOT NULL,
    IdContaDestino  INT                 NOT NULL,
    Valor           DECIMAL             NOT NULL
);

CREATE UNIQUE INDEX idx_unique_transacoes_codigo_operacao ON Transacoes (CodigoOperacao)
CREATE UNIQUE INDEX idx_unique_Transacoes_id_externo      ON Transacoes (IdExterno);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_TO_TRANSACAO
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_DESTINO_TO_TRANSACAO
        FOREIGN KEY (IdContaDestino) REFERENCES Contas (Id);

