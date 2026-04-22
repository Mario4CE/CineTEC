USE CineTecDB;
GO

-- Crear tabla de Peliculas
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
GO
