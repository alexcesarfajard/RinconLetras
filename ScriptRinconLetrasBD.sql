--Creacion de la BD RinconLetras
DROP DATABASE RinconLetrasBD;

CREATE DATABASE RinconLetrasBD;
GO

USE RinconLetrasBD;
GO

-- TABLAS 

-- T_Editoriales Tabla de editorales
CREATE TABLE Tb_Editoriales (
    IdEditorial INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Direccion VARCHAR(45)
);

-- Tb_Generos Tabla de géneros de libros
CREATE TABLE Tb_Generos (
    IdGenero INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(250)
);

-- Tb_Ubicacion Tabla de ubicación, contiene los estantes
CREATE TABLE Tb_Ubicacion (
    IdUbicacion INT IDENTITY(1,1) PRIMARY KEY,
    Numero_estante VARCHAR(45)
);

-- Tb_Autores Tabla de autores 
CREATE TABLE Tb_Autores (
    IdAutor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Autor VARCHAR(45) NOT NULL,
    Nacionalidad VARCHAR(45)
);

-- Tb_Clientes Tabla de los datos de los clientes eliminada 
/*
CREATE TABLE Tb_Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente VARCHAR(255) NOT NULL,
    TarjetaCliente BIGINT UNIQUE NOT NULL,
    Correo VARCHAR(150)
);
*/

-- Tb_Roles contiene los roles de los usuarios
CREATE TABLE Tb_Roles (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    NombreRol VARCHAR(100) NOT NULL
);

-- Tb_Usuarios Tabla usuarios contiene los datos de los usuarios
CREATE TABLE Tb_Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(45) NOT NULL,
    FechaNacimiento DATETIME,
    Identificacion INT UNIQUE,
    IdRol INT,
	Activo INT,
	CorreoElectronico varchar(200),
	Contrasenna VARCHAR(100),
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES Tb_Roles(IdRol)
);

ALTER TABLE Tb_Usuarios
ADD CONSTRAINT UQ_Usuarios_Correo UNIQUE (CorreoElectronico);

-- Tb_Libros contiene los libros de la librería
CREATE TABLE Tb_Libros (
    IdLibro INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Precio DECIMAL(8,2),
    CantidadInventario INT,
    IdEditorial INT,
    IdGenero INT,
    CONSTRAINT FK_Libro_Editorial FOREIGN KEY (IdEditorial) REFERENCES Tb_Editoriales(IdEditorial),
    CONSTRAINT FK_Libro_Genero FOREIGN KEY (IdGenero) REFERENCES Tb_Generos(IdGenero)
);

ALTER TABLE Tb_Libros
ADD Activo BIT NOT NULL DEFAULT 1,
    Imagen VARCHAR(255) NULL;

-- Tb_AutoresLibros contiene referencias de autores para un libro (un libro puede tener varios autores)
CREATE TABLE Tb_AutoresLibros (
    Id_libro INT,
    IdAutor INT,
    PRIMARY KEY (Id_libro, IdAutor),
    CONSTRAINT FK_AutoresLibro_Libro FOREIGN KEY (Id_libro) REFERENCES Tb_Libros(IdLibro),
    CONSTRAINT FK_AutoresLibro_Autor FOREIGN KEY (IdAutor) REFERENCES Tb_Autores(IdAutor)
);

-- Tb_Ubicaciones Tabla de ubicaciones tiene información de donde se guardan los libros
CREATE TABLE Tb_Ubicaciones (
    IdUbicacion INT,
    IdLibro INT,
    Cantidad INT,
    PRIMARY KEY (IdUbicacion, IdLibro),
    CONSTRAINT FK_Ubicaciones_Ubicacion FOREIGN KEY (IdUbicacion) REFERENCES Tb_Ubicacion(IdUbicacion),
    CONSTRAINT FK_Ubicaciones_Libro FOREIGN KEY (IdLibro) REFERENCES Tb_Libros(IdLibro)
);

-- Tb_Facturas tabla de facturas tiene los datos de las facturas, cliente, fecha
CREATE TABLE Tb_Facturas (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    FechaFactura DATETIME NOT NULL,
    MontoTotal DECIMAL(10,2) NOT NULL,
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_Factura_Cliente FOREIGN KEY (IdUsuario) REFERENCES Tb_Usuarios(IdUsuario) -- Cliente de factura
);

-- Tb_DetallesFacturas contiene los detalles de una factura 
CREATE TABLE Tb_DetallesFacturas (
	IdFactura INT,
    Id_libro INT,
    CantidadComprada INT,
    PRIMARY KEY (IdFactura, Id_libro),
    CONSTRAINT FK_DetalleFactura_Factura FOREIGN KEY (IdFactura) REFERENCES Tb_Facturas(IdFactura),
    CONSTRAINT FK_DetalleFactura_Libro FOREIGN KEY (Id_libro) REFERENCES Tb_Libros(IdLibro)
);
GO

-- Inserts de prueba
-- Tb_Editoriales
INSERT INTO Tb_Editoriales (Nombre, Direccion) VALUES
('Editorial Oceano', 'Av. Central #245, San José'),
('Editorial Herediano', 'Calle 8, Edificio Azul, Heredia'),
('Editorial Naranjo', 'Boulevard Morazán, Alajuela');

SELECT * FROM Tb_Editoriales;

-- Tb_Generos
INSERT INTO Tb_Generos (Nombre, Descripcion) VALUES
('Ficción', 'Libros de historias imaginarias o inventadas'),
('Ciencia', 'Libros sobre descubrimientos y conocimiento científico'),
('Historia', 'Libros sobre hechos históricos y biografías');

SELECT * FROM Tb_Generos;

-- Tb_Ubicacion (estantes)
INSERT INTO Tb_Ubicacion (Numero_estante) VALUES
('A1'),
('B2'),
('C3');

SELECT * FROM Tb_Ubicacion;

-- Tb_Autores
INSERT INTO Tb_Autores (Nombre_Autor, Nacionalidad) VALUES
('Gabriel García Márquez', 'Colombiana'),
('Stephen Hawking', 'Británica'),
('Isabel Allende', 'Chilena');

SELECT * FROM Tb_Autores;

-- Tb_Roles
INSERT INTO Tb_Roles (NombreRol) VALUES
('Usuario cliente'),
('Usuario vendedor'),
('Administrador');

SELECT * FROM Tb_Puestos;

-- Insertes en Tb_Usuarios, primeros 3 inserts de Clientes
INSERT INTO Tb_Usuarios(NombreUsuario, FechaNacimiento, Identificacion, IdRol, Activo, CorreoElectronico, Contrasenna) VALUES
('Andrés Mora', '1990-04-12', 101230456, 1, 1, 'andres@email.com', 'andres123'),
('Sofía Rojas', '1995-09-23', 101230567, 1, 1, 'sofia@email.com', 'sofia123'),
('Daniel Pérez', '1988-01-17', 101230890, 1, 1, 'daniel@email.com', 'daniel123');

-- Inserts para usario Vendedor
INSERT INTO Tb_Usuarios(NombreUsuario, FechaNacimiento, Identificacion, IdRol, Activo, CorreoElectronico, Contrasenna) VALUES
('Juan Solano', '1990-04-12', 102340567, 2, 1, 'juans@email.com', 'juan123'),
('Maria Rodriguez', '1995-09-23', 102340789, 2, 1, 'mariar@email.com', 'maria123');

-- Insert para usuario Administrador
INSERT INTO Tb_Usuarios(NombreUsuario, FechaNacimiento, Identificacion, IdRol, Activo, CorreoElectronico, Contrasenna) VALUES
('Alex Cesar', '1996-08-27', 201230456, 3, 1, 'alexc@email.com', 'alex123');

INSERT INTO Tb_Usuarios(NombreUsuario, FechaNacimiento, Identificacion, IdRol, Activo, CorreoElectronico, Contrasenna) VALUES
('Alex Fajardo', '1996-08-27', 201230451, 3, 1, 'alexf@email.com', 'alex123');


SELECT * FROM Tb_Usuarios;

SELECT 
    u.IdUsuario,
    u.NombreUsuario,
	u.FechaNacimiento,
	u.Identificacion,
	u.IdRol,
    r.NombreRol,
	u.activo,
	u.CorreoElectronico,
	u.Contrasenna
FROM Tb_Usuarios u
INNER JOIN Tb_Roles r
    ON u.IdRol = r.IdRol;

-- Tb_Libros
INSERT INTO Tb_Libros (Nombre, Precio, CantidadInventario, IdEditorial, IdGenero) VALUES
('Cien años de soledad', 12000.00, 10, 1, 1),
('Breve historia del tiempo', 15000.00, 8, 3, 2),
('La casa de los espíritus', 11000.00, 12, 2, 1);

-- Tb_AutoresLibros
INSERT INTO Tb_AutoresLibros (Id_libro, IdAutor) VALUES
(1, 1),  -- "Cien años de soledad" - Gabriel García Márquez
(2, 2),  -- "Breve historia del tiempo" - Stephen Hawking
(3, 3);  -- "La casa de los espíritus" - Isabel Allende

-- Tb_Ubicaciones
INSERT INTO Tb_Ubicaciones (IdUbicacion, IdLibro, Cantidad) VALUES
(1, 1, 1),  -- "Cien años de soledad" en estante A1
(2, 2, 2),  -- "Breve historia del tiempo" en estante B2
(3, 3, 3);  -- "La casa de los espíritus" en estante C3

-- Procedimientos almacenados

-- SP para validar si existe un usuario y si sus credenciales son correctas para iniciar sesión
drop procedure ValidarUsuario;

CREATE PROCEDURE ValidarUsuario
    @CorreoElectronico  VARCHAR(100), 
    @Contrasenna        VARCHAR(10)
AS
BEGIN
    SELECT  IdUsuario,
            NombreUsuario,
            CorreoElectronico,
            Contrasenna,
            Activo,
            Identificacion,
			IdRol
    FROM    dbo.Tb_Usuarios
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Activo = 1
END
GO

EXEC ValidarUsuario 'andres@email.com', 'andres123';

drop procedure RegistrarCliente;

-- Registrar nuevo cliente
CREATE PROCEDURE RegistrarCliente
    @NombreUsuario     VARCHAR(45),
    @FechaNacimiento   DATE,
    @Identificacion    INT,
    @CorreoElectronico VARCHAR(200),
    @Contrasena        VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Tb_Usuarios (
            NombreUsuario, FechaNacimiento, Identificacion,
            IdRol, Activo, CorreoElectronico,
            Contrasenna
        )
        VALUES (
            @NombreUsuario,
            @FechaNacimiento,
            @Identificacion,
            1,               -- Rol fijo: Cliente
            1,				 -- Activo = 1 
            @CorreoElectronico,
            @Contrasena
        );
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR('Error al registrar cliente: %s', 16, 1, @ErrorMessage);
    END CATCH
END;
GO

EXEC RegistrarCliente
    @NombreUsuario = 'Jose Garcia',
    @FechaNacimiento = '1990-05-12',
    @Identificacion = 102340123,
    @CorreoElectronico = 'juang@email.com',
    @Contrasena = 'juan123';

-- SP para consultar libros
CREATE PROCEDURE ConsultarCatalogoLibros
AS
BEGIN
    SELECT 
        L.IdLibro,
        L.Nombre AS Titulo,
        L.Precio,
        L.CantidadInventario,
        E.Nombre AS Editorial,
        G.Nombre AS Genero,
        A.Nombre_Autor AS Autor,
        U.Numero_estante AS Estante
    FROM Tb_Libros L
    INNER JOIN Tb_Editoriales E ON L.IdEditorial = E.IdEditorial
    INNER JOIN Tb_Generos G ON L.IdGenero = G.IdGenero
    INNER JOIN Tb_AutoresLibros AL ON L.IdLibro = AL.Id_libro
    INNER JOIN Tb_Autores A ON AL.IdAutor = A.IdAutor
    INNER JOIN Tb_Ubicaciones UB ON L.IdLibro = UB.IdLibro
    INNER JOIN Tb_Ubicacion U ON UB.IdUbicacion = U.IdUbicacion;
END
GO

EXEC ConsultarCatalogoLibros;