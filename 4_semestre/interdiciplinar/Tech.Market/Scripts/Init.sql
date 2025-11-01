CREATE DATABASE TechMarketDB;

CREATE TABLE Contas
(
    Id          SERIAL       PRIMARY KEY,
    IdExterno   UUID NOT NULL DEFAULT gen_random_uuid(),
    Nome        VARCHAR(150) NOT NULL,
    CPF         VARCHAR(20)  NOT NULL
);

CREATE UNIQUE INDEX idx_unique_contas_id_externo ON Contas (IdExterno);

INSERT INTO public.contas(nome, cpf)
VALUES('Cleiton', '12345678909'), ('Paula', '71875805095');

CREATE TABLE Saldos
(
    Id          SERIAL PRIMARY KEY,
    IdExterno   UUID NOT NULL DEFAULT gen_random_uuid(),
    Valor       DECIMAL NOT NULL,
    IdConta     INT NOT NULL 
);

ALTER TABLE Saldos  
    ADD CONSTRAINT FK_CONTA_TO_SALDOS
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

CREATE UNIQUE INDEX idx_unique_saldos_id_conta ON Saldos (IdConta);
CREATE UNIQUE INDEX idx_unique_saldos_id_externo ON Saldos (IdExterno);


INSERT INTO Saldos(Valor, IdConta)
VALUES (10.1, 1), (13.13, 2);


CREATE TABLE Transacoes
(
    Id              SERIAL       PRIMARY KEY,
    IdExterno       UUID NOT NULL DEFAULT gen_random_uuid(),
    CodigoOperacao  UUID        NOT NULL DEFAULT gen_random_uuid(),
    IdConta         INT NOT NULL,
    IdContaDestino  INT NOT NULL,
    Valor           DECIMAL     NOT NULL
);

CREATE UNIQUE INDEX idx_unique_transacoes_codigo_operacao ON Transacoes (CodigoOperacao)
CREATE UNIQUE INDEX idx_unique_Transacoes_id_externo      ON Transacoes (IdExterno);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_TO_TRANSACAO
        FOREIGN KEY (IdConta) REFERENCES Contas (Id);

ALTER TABLE Transacoes  
    ADD CONSTRAINT FK_CONTA_DESTINO_TO_TRANSACAO
        FOREIGN KEY (IdContaDestino) REFERENCES Contas (Id);

