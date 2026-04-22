USE CineTecDB;
GO

-- Crear tabla de Proyecciones
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
