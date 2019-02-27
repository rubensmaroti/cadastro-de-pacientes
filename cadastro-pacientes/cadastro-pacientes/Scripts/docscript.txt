CREATE DATABASE CadastroDePacienteDB
GO
USE CadastroDePacienteDB
GO
CREATE TABLE Paciente
(
    Indice int IDENTITY(1,1)  not null  , 
	CPF varchar (11) not null primary key,
	Nome varchar(100) not null,
	Sexo varchar(1) not null,
	DtNascimento datetime not null,
	Email varchar(50) not null,
	Telefone varchar(11) not null,	
)
GO
CREATE TABLE Imagens
(
	Caminho varchar(900) not null primary key,
	CPF varchar (11) not null , 
	CONSTRAINT fk_Pacientecpf FOREIGN KEY (CPF) References Paciente (CPF)	 
)