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

INSERT INTO Saldos(Valor, IdConta)
VALUES (10.1, 1), (13.13, 2);


CREATE TABLE Transacoes
(
    Id              SERIAL      PRIMARY KEY,
    CodigoOperacao  UUID        NOT NULL DEFAULT gen_random_uuid(),
    IdConta         INT NOT NULL,
    Saida           BOOL        NOT NULL,
    Valor           DECIMAL     NOT NULL
);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_TO_TRANSACAO
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

