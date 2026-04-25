CREATE DATABASE CineTecDB;
GO

USE CineTecDB;
GO

// Crear tabla de Administradores 
CREATE TABLE Administradores ( 
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL DEFAULT 'Admin'
);

// Crear tabla de Peliculas
CREATE TABLE Peliculas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreOriginal NVARCHAR(150) NOT NULL UNIQUE,
    NombreComercial NVARCHAR(150) NOT NULL UNIQUE,
    Imagen NVARCHAR(255) NULL,
    Duracion INT NOT NULL,
    Protagonistas NVARCHAR(255) NULL,
    Director NVARCHAR(100) NULL,
    Clasificacion NVARCHAR(50) NULL
);

// Crear tabla de Sucursales
CREATE TABLE Sucursales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Ubicacion NVARCHAR(200) NOT NULL,
    CantidadSalas INT NOT NULL
);

// Crear tabla de Salas
CREATE TABLE Salas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Identificador NVARCHAR(50) NOT NULL ,
    SucursalId INT NOT NULL,
    Filas INT NOT NULL,
    Columnas INT NOT NULL,
    Capacidad INT NOT NULL,
    CONSTRAINT FK_Salas_Sucursales FOREIGN KEY (SucursalId)
        REFERENCES Sucursales(Id)
);

// Crear tabla de Proyecciones
CREATE TABLE Proyecciones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PeliculaId INT NOT NULL,
    SalaId INT NOT NULL,
    Fecha DATETIME NOT NULL,
    CONSTRAINT FK_Proyecciones_Peliculas FOREIGN KEY (PeliculaId)
        REFERENCES Peliculas(Id),
    CONSTRAINT FK_Proyecciones_Salas FOREIGN KEY (SalaId)
        REFERENCES Salas(Id)
);
GO

INSERT INTO Administradores (Nombre, Usuario, Contrasena, Rol)
VALUES ('Administrador Principal', 'admin', '1234', 'Admin'); // Valores de ejemplo
GO