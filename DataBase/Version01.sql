CREATE DATABASE IF NOT EXISTS `interfell`;
USE `interfell`;


CREATE TABLE Pais (
  IdPais INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  NomPais VARCHAR(255) NOT NULL,
  SiglaPais VARCHAR(3) NULL,
  Moneda VARCHAR(5) NOT NULL,
  PRIMARY KEY(IdPais)
);

CREATE TABLE Persona (
  Identificacion VARCHAR(255) NOT NULL,
  IdPersona INTEGER UNSIGNED NOT NULL,
  TipoIdentificacion INTEGER UNSIGNED NOT NULL,
  PrimerNombre VARCHAR(50) NOT NULL,
  SegundoNombre VARCHAR(50) NULL,
  PrimerApellido VARCHAR(50) NOT NULL,
  SegundoApellido VARCHAR(50) NULL,
  ComunaId INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(Identificacion)
);

CREATE TABLE Roles (
  IdRol INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  NomRol VARCHAR(50) NOT NULL,
  SiglaRol VARCHAR(5) NOT NULL,
  PRIMARY KEY(IdRol)
);

CREATE TABLE Comuna (
  IdComuna INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  CodRegion INTEGER UNSIGNED NOT NULL,
  NomComuna VARCHAR(255) NOT NULL,
  PRIMARY KEY(IdComuna),
  INDEX Comuna_FKIndex1(CodRegion)
);


CREATE TABLE Ayudas (
  IdAyuda INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  NomAyuda VARCHAR(50) NOT NULL,
  EsRegional BOOL NULL,
  EsComunal BOOL NULL,
  Valor INTEGER UNSIGNED NULL,
  Moneda VARCHAR(5) NULL,
  PRIMARY KEY(IdAyuda)
);

CREATE TABLE Region (
  IdRegion INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  PaisId INTEGER UNSIGNED NOT NULL,
  NomRegion VARCHAR(255) NOT NULL,
  SiglaRegion VARCHAR(255) NULL,
  PRIMARY KEY(IdRegion),
  INDEX Region_FKIndex1(PaisId)
);

CREATE TABLE Users (
  UserId INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  FullName VARCHAR(4000) NOT NULL,
  UserName VARCHAR(512) NOT NULL,
  UserPsw VARCHAR(4000) NOT NULL,
  RolId INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(UserId),
  INDEX Users_FKIndex1(RolId)
);

CREATE TABLE Ayudas_Region (
  RegionId INTEGER UNSIGNED NOT NULL,
  AyudaId INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(RegionId, AyudaId),
  INDEX Ayudas_has_Region_FKIndex1(AyudaId),
  INDEX Ayudas_has_Region_FKIndex2(RegionId)
);

CREATE TABLE Ayudas_Comuna (
  ComunaId INTEGER UNSIGNED NOT NULL,
  AyudaId INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(ComunaId, AyudaId),
  INDEX Ayudas_has_Comuna_FKIndex1(AyudaId),
  INDEX Ayudas_has_Comuna_FKIndex2(ComunaId)
);

CREATE TABLE Persona_Ayudas (
  IdentificacionPersona VARCHAR(255) NOT NULL,
  AyudaId INTEGER UNSIGNED NOT NULL,
  Año INTEGER UNSIGNED NULL,
  PRIMARY KEY(IdentificacionPersona, AyudaId),
  INDEX Persona_has_Ayudas_FKIndex1(IdentificacionPersona),
  INDEX Persona_has_Ayudas_FKIndex2(AyudaId),
  UNIQUE INDEX PersonaAyudas_UK(Año, IdentificacionPersona, AyudaId)
);

CREATE TABLE Logger (
  IdLogger INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  Fecha DATE NOT NULL,
  Hora TIME NOT NULL,
  Accion VARCHAR(50) NOT NULL,
  UserId INTEGER UNSIGNED NOT NULL,
  Entidad VARCHAR(255) NOT NULL,
  PrevData LONGTEXT NULL,
  PostData LONGTEXT NULL,
  PRIMARY KEY(IdLogger),
  INDEX Logger_FKIndex1(UserId)
);


INSERT INTO `interfell`.`Roles` (`NomRol`, `SiglaRol`) VALUES ('Administrador', 'admin');
INSERT INTO `interfell`.`Roles` (`NomRol`, `SiglaRol`) VALUES ('Usuario', 'user');

INSERT INTO `interfell`.`Pais` (`NomPais`, `SiglaPais`, `Moneda`) VALUES ('Colombia', 'COL', 'COP');
INSERT INTO `interfell`.`Region` (`PaisId`, `NomRegion`, `SiglaRegion`) VALUES (1, 'Bogota', 'BOG');
INSERT INTO `interfell`.`Comuna` (`CodRegion`, `NomComuna`) VALUES (1, 'Quiroga');

