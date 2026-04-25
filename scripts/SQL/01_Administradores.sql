USE CineTecDB;
GO

-- Crear tabla de Administradores
CREATE TABLE Administradores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL DEFAULT 'Admin'
);
GO
