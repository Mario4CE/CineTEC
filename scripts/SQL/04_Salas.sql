USE CineTecDB;
GO

-- Crear tabla de Salas
CREATE TABLE Salas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Identificador NVARCHAR(50) NOT NULL,
    SucursalId INT NOT NULL,
    Filas INT NOT NULL,
    Columnas INT NOT NULL,
    Capacidad INT NOT NULL,
    CONSTRAINT FK_Salas_Sucursales FOREIGN KEY (SucursalId)
        REFERENCES Sucursales(Id)
);
GO
