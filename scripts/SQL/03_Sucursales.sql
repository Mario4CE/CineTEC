USE CineTecDB;
GO

-- Crear tabla de Sucursales
CREATE TABLE Sucursales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Ubicacion NVARCHAR(200) NOT NULL,
    CantidadSalas INT NOT NULL
);
GO
