CREATE DATABASE TechMarketDB;

USE TechMarketDB;

CREATE TABLE Contas
(
    Id      SERIAL PRIMARY KEY,
    Nome    VARCHAR(150) NOT NULL
);

INSERT INTO public.contas(nome)
VALUES('Cleiton'), ('Paula');

CREATE TABLE Saldos
(
    Id      SERIAL PRIMARY KEY,
    Valor   DECIMAL NOT NULL,
    IdConta INT NOT NULL 
);

ALTER TABLE Saldos  
    ADD CONSTRAINT FK_CONTA_TO_SALDOS
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

CREATE UNIQUE INDEX idx_unique_saldos_id_conta ON Saldos (IdConta);

INSERT INTO Saldos(Valor, IdConta)
VALUES (10.1, 1), (13.13, 2);


CREATE TABLE Transacoes
(
    Id              SERIAL      PRIMARY KEY,
    CodigoOperacao  UUID        NOT NULL DEFAULT gen_random_uuid(),
    IdConta         INT NOT NULL,
    IdContaDestino  INT NOT NULL,
    Valor           DECIMAL     NOT NULL
);

CREATE UNIQUE INDEX idx_unique_transacoes_codigo_operacao ON Transacoes (CodigoOperacao)

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_TO_TRANSACAO
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_DESTINO_TO_TRANSACAO
        FOREIGN KEY (IdContaDestino) REFERENCES Contas (Id);

