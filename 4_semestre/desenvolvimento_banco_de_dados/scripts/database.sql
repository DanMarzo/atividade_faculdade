-- -----------------------------------------------------
-- Schema loja
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `loja` DEFAULT CHARACTER SET utf8 ;
USE `loja` ;

-- -----------------------------------------------------
-- Table `loja`.`Estado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja`.`Estado` 
(
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(80) NOT NULL,
  `UF` CHAR(2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `loja`.`Municipio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja`.`Municipio` 
(
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Estado_Id` INT NOT NULL,
  `Nome` VARCHAR(80) NOT NULL,
  `CodIBGE` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Municipio_Estado1_idx` (`Estado_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Municipio_Estado1`
    FOREIGN KEY (`Estado_Id`)
    REFERENCES `loja`.`Estado` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `loja`.`Cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja`.`Cliente` 
(
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(80) NOT NULL,
  `CPF` CHAR(11) NOT NULL,
  `Celular` CHAR(11) NULL,
  `EndLogradouro` VARCHAR(100) NOT NULL,
  `EndNumero` VARCHAR(10) NOT NULL,
  `EndMunicipio` INT NOT NULL,
  `EndCEP` CHAR(8) NULL,
  `Municipio_Id` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Cliente_Municipio1_idx` (`Municipio_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Cliente_Municipio1`
    FOREIGN KEY (`Municipio_Id`)
    REFERENCES `loja`.`Municipio` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `loja`.`ContaReceber`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja`.`ContaReceber` 
(
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Cliente_Id` INT NOT NULL,
  `FaturaVendaId` INT NULL,
  `DataConta` DATE NOT NULL,
  `DataVencimento` DATE NOT NULL,
  `Valor` DECIMAL(18,2) NOT NULL,
  `Situacao` ENUM('1', '2', '3') NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_ContaReceber_Cliente_idx` (`Cliente_Id` ASC) VISIBLE,
  CONSTRAINT `fk_ContaReceber_Cliente`
    FOREIGN KEY (`Cliente_Id`)
    REFERENCES `loja`.`Cliente` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB;