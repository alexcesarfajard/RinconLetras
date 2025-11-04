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

-- Tb_Clientes Tabla de los datos de los clientes
CREATE TABLE Tb_Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente VARCHAR(45) NOT NULL,
    TarjetaCliente INT UNIQUE NOT NULL,
    Correo VARCHAR(150)
);

-- Tb_Puestos contiene los puestos de los empleados
CREATE TABLE Tb_Puestos (
    IdPuesto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_puesto VARCHAR(45) NOT NULL
);

-- Tb_Empleados Tabla empleados contiene los datos de los empleados
CREATE TABLE Tb_Empleados (
    IdEmpleados INT IDENTITY(1,1) PRIMARY KEY,
    NombreEmpleado VARCHAR(45) NOT NULL,
    FechaNacimiento DATETIME,
    Salario DECIMAL(10,2),
    NumeroCarnet INT UNIQUE,
    IdPuesto INT,
	Activo INT,
	CorreoElectronico varchar(200),
	Contrasenna VARCHAR(50),
    CONSTRAINT FK_Empleado_Puesto FOREIGN KEY (IdPuesto) REFERENCES Tb_Puestos(IdPuesto)
);

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

-- Tb_Facturas tabla de facturas tiene los datos de las facturas, cliente, fecha, 
CREATE TABLE Tb_Facturas (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    FechaFactura DATETIME NOT NULL,
    MontoTotal DECIMAL(10,2) NOT NULL,
    IdCliente INT NOT NULL,
    IdEmpleados INT NOT NULL,
    CONSTRAINT FK_Factura_Cliente FOREIGN KEY (IdCliente) REFERENCES Tb_Clientes(IdCliente),
    CONSTRAINT FK_Factura_Empleado FOREIGN KEY (IdEmpleados) REFERENCES Tb_Empleados(IdEmpleados)
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

-- Tb_Clientes
INSERT INTO Tb_Clientes (NombreCliente, TarjetaCliente, Correo) VALUES
('Laura Jiménez', '103210654', 'laura.jimenez@email.com'),
('Carlos Soto', '407890456', 'carlos.soto@email.com'),
('María Vargas', '701590236', 'maria.vargas@email.com');

SELECT * FROM Tb_Clientes;

-- Tb_Puestos
INSERT INTO Tb_Puestos (Nombre_puesto) VALUES
('Cajero'),
('Vendedor'),
('Administrador');

SELECT * FROM Tb_Puestos;

-- Tb_Empleados
INSERT INTO Tb_Empleados (NombreEmpleado, FechaNacimiento, Salario, NumeroCarnet, IdPuesto, Contrasenna, Activo, CorreoElectronico) VALUES
('Andrés Mora', '1990-04-12', 550000.00, 1001, 1, 'andres123', 1, 'andres@email.com'),
('Sofía Rojas', '1995-09-23', 620000.00, 1002, 2, 'sofia123', 1, 'sofia@email.com'),
('Daniel Pérez', '1988-01-17', 800000.00, 1003, 3, 'daniel123', 1, 'daniel@email.com');

SELECT * FROM Tb_Empleados;

-- Tb_Libros
INSERT INTO Tb_Libros (Nombre, Precio, CantidadInventario, IdEditorial, IdGenero) VALUES
('Cien años de soledad', 12000.00, 10, 1, 1),
('Breve historia del tiempo', 15000.00, 8, 3, 2),
('La casa de los espíritus', 11000.00, 12, 2, 1);

-- Tb_AutoresLibros
INSERT INTO Tb_AutoresLibros (Id_libro, IdAutor) VALUES
(1, 7),  -- "Cien años de soledad" - Gabriel García Márquez
(2, 8),  -- "Breve historia del tiempo" - Stephen Hawking
(3, 9);  -- "La casa de los espíritus" - Isabel Allende

-- Tb_Ubicaciones
INSERT INTO Tb_Ubicaciones (IdUbicacion, IdLibro, Cantidad) VALUES
(7, 1, 4),  -- "Cien años de soledad" en estante A1
(8, 2, 3),  -- "Breve historia del tiempo" en estante B2
(9, 3, 5);  -- "La casa de los espíritus" en estante C3

-- Procedimientos almacenados

CREATE PROCEDURE ValidarEmpleado
    @CorreoElectronico  VARCHAR(100), 
    @Contrasenna        VARCHAR(10)
AS
BEGIN
    SELECT  IdEmpleados,
            NombreEmpleado,
            CorreoElectronico,
            Contrasenna,
            Activo,
            NumeroCarnet
    FROM    dbo.Tb_Empleados
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Activo = 1
END
GO

SELECT * FROM Tb_Empleados;

EXEC ValidarEmpleado 'andres@email.com', 'andres123';

