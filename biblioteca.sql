CREATE DATABASE Biblioteca;
USE Biblioteca;

CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Rol ENUM('Admin', 'Bibliotecario', 'Lector') NOT NULL,
    FechaIngreso DATE NOT NULL
);

CREATE TABLE Libros (
    LibroID INT PRIMARY KEY AUTO_INCREMENT,
    Titulo VARCHAR(100) NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    Editorial VARCHAR(50),
    AnioPublicacion INT,
    Genero VARCHAR(50),
    CopiasDisponibles INT NOT NULL,
    CopiasTotales INT NOT NULL
);

CREATE TABLE Transacciones (
    TransaccionID INT PRIMARY KEY AUTO_INCREMENT,
    UsuarioID INT NULL, -- Permitir valores NULL
    LibroID INT NULL,   -- Permitir valores NULL
    FechaPrestamo DATE NOT NULL,
    FechaDevolucion DATE,
    Estado ENUM('Prestado', 'Devuelto') NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID) ON DELETE SET NULL,
    FOREIGN KEY (LibroID) REFERENCES Libros(LibroID) ON DELETE SET NULL
);

CREATE TABLE Login (
    LoginID INT PRIMARY KEY AUTO_INCREMENT,
    UsuarioID INT,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);


INSERT INTO usuarios(Nombre, Apellido, rol, FechaIngreso) VALUES ('Cristobal', 'Rodas', 'Admin', CURRENT_TIMESTAMP);
INSERT INTO usuarios(Nombre, Apellido, rol, FechaIngreso) VALUES ('Saraí', 'Parada', 'Bibliotecario', CURRENT_TIMESTAMP);
INSERT INTO Login(UsuarioID, Username, PasswordHash) VALUES (1, 'Admin', 'admin');
INSERT INTO Login(UsuarioID, Username, PasswordHash) VALUES (2, 'Sarita', '123');

INSERT INTO Libros (Titulo, Autor, Editorial, AnioPublicacion, Genero, CopiasDisponibles, CopiasTotales) VALUES
('Cien Años de Soledad', 'Gabriel García Márquez', 'Sudamericana', 1967, 'Realismo Mágico', 5, 5),
('1984', 'George Orwell', 'Secker & Warburg', 1949, 'Distopía', 7, 10),
('El Principito', 'Antoine de Saint-Exupéry', 'Reynal & Hitchcock', 1943, 'Fábula', 6, 8),
('Don Quijote de la Mancha', 'Miguel de Cervantes', 'Francisco de Robles', 1605, 'Novela', 3, 5),
('Orgullo y Prejuicio', 'Jane Austen', 'T. Egerton', 1813, 'Romance', 4, 6),
('Crimen y Castigo', 'Fiódor Dostoyevski', 'The Russian Messenger', 1866, 'Filosófico', 8, 10),
('Rayuela', 'Julio Cortázar', 'Sudamericana', 1963, 'Novela', 5, 8),
('La Metamorfosis', 'Franz Kafka', 'Kurt Wolff Verlag', 1915, 'Existencialismo', 9, 9),
('Ulises', 'James Joyce', 'Sylvia Beach', 1922, 'Modernismo', 2, 4),
('Matar a un Ruiseñor', 'Harper Lee', 'J.B. Lippincott & Co.', 1960, 'Drama', 5, 7),
('Los Miserables', 'Victor Hugo', 'A. Lacroix', 1862, 'Histórica', 6, 6),
('En Busca del Tiempo Perdido', 'Marcel Proust', 'Grasset', 1913, 'Modernismo', 5, 10),
('El Gran Gatsby', 'F. Scott Fitzgerald', 'Scribner', 1925, 'Tragedia', 4, 6),
('La Odisea', 'Homero', 'Escritos Griegos Antiguos', -800, 'Épica', 3, 5),
('La Divina Comedia', 'Dante Alighieri', 'Niccolò di Lorenzo', 1472, 'Épico', 5, 5),
('El Nombre de la Rosa', 'Umberto Eco', 'Bompiani', 1980, 'Misterio', 8, 10),
('Cumbres Borrascosas', 'Emily Brontë', 'Thomas Cautley Newby', 1847, 'Romance Gótico', 7, 7),
('Frankenstein', 'Mary Shelley', 'Lackington, Hughes, Harding, Mavor & Jones', 1818, 'Horror', 6, 8),
('El Retrato de Dorian Gray', 'Oscar Wilde', 'Ward, Lock & Co.', 1890, 'Fantasía', 4, 6),
('Fahrenheit 451', 'Ray Bradbury', 'Ballantine Books', 1953, 'Ciencia Ficción', 5, 5);