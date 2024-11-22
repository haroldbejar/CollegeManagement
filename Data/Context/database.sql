-- Crear la base de datos
CREATE DATABASE CollegeManagementDb;
GO

-- Usar la base de datos
USE CollegeManagementDb;
GO

-- Crear tabla Profesor
CREATE TABLE Profesores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Genero NVARCHAR(10) NOT NULL
);

-- Crear tabla Grado
CREATE TABLE Grados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    ProfesorId INT NOT NULL, -- Relación con Profesor
    CONSTRAINT FK_Grados_Profesores FOREIGN KEY (ProfesorId)
        REFERENCES Profesores(Id)
        ON DELETE CASCADE
);

-- Crear tabla Alumno con el campo FechaNacimiento agregado
CREATE TABLE Alumnos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Genero NVARCHAR(10) NOT NULL,
    FechaNacimiento DATETIME NOT NULL 
);

-- Crear tabla AlumnoGrado
CREATE TABLE AlumnosGrados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoId INT NOT NULL, -- Relación con Alumno
    GradoId INT NOT NULL, -- Relación con Grado
    Grupo NVARCHAR(10) NOT NULL,
    CONSTRAINT FK_AlumnosGrados_Alumnos FOREIGN KEY (AlumnoId)
        REFERENCES Alumnos(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_AlumnosGrados_Grados FOREIGN KEY (GradoId)
        REFERENCES Grados(Id)
        ON DELETE CASCADE
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,        -- Llave primaria con autoincremento
    UserName NVARCHAR(100) NOT NULL,         -- Nombre de usuario (obligatorio)
    PasswordHash VARBINARY(MAX) NOT NULL,    -- Hash de la contraseña (obligatorio)
    PasswordSalt VARBINARY(MAX) NOT NULL,    -- Salt de la contraseña (obligatorio)
    Role NVARCHAR(50) NOT NULL               -- Rol del usuario (obligatorio)
);

-- Opcional: Población inicial de datos
-- INSERT INTO Profesores (Nombre, Genero) VALUES ('Juan Pérez', 'Masculino');
-- INSERT INTO Grados (Nombre, ProfesorId) VALUES ('Matemáticas', 1);
-- INSERT INTO Alumnos (Nombre, Apellido, Genero, FechaNacimiento) VALUES ('Ana', 'Gómez', 'Femenino', '2005-05-15');
-- INSERT INTO AlumnosGrados (AlumnoId, GradoId, Grupo) VALUES (1, 1, 'A');


