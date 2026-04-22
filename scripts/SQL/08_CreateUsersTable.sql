-- Crear tabla de Usuarios en CineTecBD
USE CineTecBD;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuarios')
BEGIN
    CREATE TABLE Usuarios (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        Cedula NVARCHAR(20) NOT NULL UNIQUE,
        Telefono NVARCHAR(20),
        FechaNacimiento DATE NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        Activo BIT DEFAULT 1
    );
    
    PRINT 'Tabla Usuarios creada exitosamente';
END
ELSE
BEGIN
    PRINT 'La tabla Usuarios ya existe';
END
GO

-- Crear indice en Cedula para búsquedas más rápidas
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Usuarios_Cedula')
BEGIN
    CREATE INDEX IX_Usuarios_Cedula ON Usuarios(Cedula);
END
GO
