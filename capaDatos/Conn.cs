using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using Cache;
using System.ComponentModel;

namespace capaDatos {
    public  class Conn {
        private readonly string connectionString;

        public Conn() {
            string server = "localhost";
            string db = "bilioteca";
            string user = "root";
            string pwd = "123456";
            connectionString = $"server={server};Port={3306};database={db};Uid={user};pwd={pwd};";
        }

        protected MySqlConnection GetConnection() {
            return new MySqlConnection(connectionString);
        }

        public bool InsertarLogin(int id, string name, string pass) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Login(UsuarioID, Username, PasswordHash) VALUES (@id, @usuario, @pass);", connection)) {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@usuario", name);
                    command.Parameters.AddWithValue("@pass", pass);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool modificarLogin(int id, string name, string pass) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Login  SET Username=@usuario, PasswordHash=@pass) WHERE UsuarioID=@id;", connection)) {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@usuario", name);
                    command.Parameters.AddWithValue("@pass", pass);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public  Login validarUsuario(string user, string pass) {
            using (var conection = GetConnection()) {
                conection.Open();
                using (var comandDB = new MySqlCommand()) {
                    comandDB.Connection = conection;
                    comandDB.CommandText = "SELECT * FROM login WHERE Username=@usuario AND PasswordHash=@pass;";
                    comandDB.Parameters.AddWithValue("@usuario", user);
                    comandDB.Parameters.AddWithValue("@pass", pass);
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Login login = new Login {
                                LoginID = reader.GetInt32(0),
                                UsuarioID = reader.GetInt32(1),
                                Username = reader.GetString(2),
                                PasswordHash = reader.GetString(3)
                            };
                            return login;
                        }
                    }
                    return null;
                    
                }
            }
        }

        public List<Usuario> ObtenerUsuarios() {
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand("SELECT * FROM Usuarios;", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Usuario usuario = new Usuario {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Rol = reader.GetString(3),
                                FechaIngreso = reader.GetDateTime(4)
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listaUsuarios;
        }

        public Usuario ObtenerUsuario(int userID) {
            Usuario usuario = new Usuario();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT * FROM Usuarios WHERE UsuarioID={userID};", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            usuario = new Usuario {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Rol = reader.GetString(3),
                                FechaIngreso = reader.GetDateTime(4)
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public Usuario ObtenerUsuario(string nombre, string apellido) {
            Usuario usuario = new Usuario();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT * FROM Usuarios WHERE Nombre=@nombre and Apellido=@apellido;", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;
                    comandDB.Parameters.AddWithValue("@nombre", nombre);
                    comandDB.Parameters.AddWithValue("@apellido", apellido);

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            usuario = new Usuario {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Rol = reader.GetString(3),
                                FechaIngreso = reader.GetDateTime(4)
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public bool InsertarUsuario(string name, string apellido, string rol) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO usuarios(Nombre, Apellido, rol, FechaIngreso) VALUES (@name, @apellido, @rol, CURRENT_TIMESTAMP);", connection)) {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@rol", rol);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool EditarUsuario(int id, string name, string apellido, string rol) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE usuarios SET	Nombre = @name, Apellido = @apellido, Rol = @rol WHERE UsuarioID = @id;", connection)) {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@rol", rol);
                    command.Parameters.AddWithValue("@id", id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool EliminarUsuario(int UsuarioID) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM usuarios WHERE UsuarioID = @UsuarioID;", connection)) {
                    command.Parameters.AddWithValue("@UsuarioID", UsuarioID);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public List<Libro> ObtenerLibros() {
            List<Libro> listaLibros = new List<Libro>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand("SELECT * FROM Libros;", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Libro libro = new Libro {
                                LibroID = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Autor = reader.GetString(2),
                                Editorial = reader.GetString(3),
                                AnioPublicacion = reader.GetInt32(4),
                                Genero = reader.GetString(5),
                                CopiasDisponibles = reader.GetInt32(6),
                                CopiasTotales = reader.GetInt32(7)
                            };

                            listaLibros.Add(libro);
                        }
                    }
                }
            }

            return listaLibros;
        }

        public Libro ObtenerLibro(int id) {
            Libro libro = new Libro();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT * FROM Libros WHERE LibroID = {id};", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            libro = new Libro {
                                LibroID = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Autor = reader.GetString(2),
                                Editorial = reader.GetString(3),
                                AnioPublicacion = reader.GetInt32(4),
                                Genero = reader.GetString(5),
                                CopiasDisponibles = reader.GetInt32(6),
                                CopiasTotales = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }

            return libro;
        }

        public List<Libro> ordenarLibros(string colum, string direc) {
            List<Libro> listaLibros = new List<Libro>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT * FROM libros ORDER BY {colum} {direc};", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Libro libro = new Libro {
                                LibroID = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Autor = reader.GetString(2),
                                Editorial = reader.GetString(3),
                                AnioPublicacion = reader.GetInt32(4),
                                Genero = reader.GetString(5),
                                CopiasDisponibles = reader.GetInt32(6),
                                CopiasTotales = reader.GetInt32(7)
                            };

                            listaLibros.Add(libro);
                        }
                    }
                }
            }

            return listaLibros;
        }

        public List<Libro> buscarLibros(string op, string buscar) {
            List<Libro> listaLibros = new List<Libro>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT * FROM Libros WHERE {op} LIKE @buscar;", connection)) {
                    comandDB.Parameters.AddWithValue("@buscar", "%" + buscar + "%");
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Libro libro = new Libro {
                                LibroID = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Autor = reader.GetString(2),
                                Editorial = reader.GetString(3),
                                AnioPublicacion = reader.GetInt32(4),
                                Genero = reader.GetString(5),
                                CopiasDisponibles = reader.GetInt32(6),
                                CopiasTotales = reader.GetInt32(7)
                            };

                            listaLibros.Add(libro);
                        }
                    }
                }
            }

            return listaLibros;
        }

        public bool EliminarLibro(int libroID) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM Libros WHERE LibroID = @LibroID;", connection)) {
                    command.Parameters.AddWithValue("@LibroID", libroID);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool InsertarLibro(Libro libro) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Libros (Titulo, Autor, Editorial, AnioPublicacion, Genero, CopiasDisponibles, CopiasTotales) VALUES(@titulo, @autor, @editorial, @publicacion, @genero, @copDisponibles, @copTotales)", connection)) {
                    command.Parameters.AddWithValue("@titulo", libro.Titulo);
                    command.Parameters.AddWithValue("@autor", libro.Autor);
                    command.Parameters.AddWithValue("@editorial", libro.Editorial);
                    command.Parameters.AddWithValue("@publicacion", libro.AnioPublicacion);
                    command.Parameters.AddWithValue("@genero", libro.Genero);
                    command.Parameters.AddWithValue("@copDisponibles", libro.CopiasDisponibles);
                    command.Parameters.AddWithValue("@copTotales", libro.CopiasTotales);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool EditarLibro(Libro libro) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Libros SET Titulo = @titulo, Autor = @autor, Editorial = @editorial, AnioPublicacion = @publicacion, Genero = @genero, CopiasDisponibles = @disponibles, CopiasTotales = @totales WHERE LibroID = @id;", connection)) {
                    command.Parameters.AddWithValue("@titulo", libro.Titulo);
                    command.Parameters.AddWithValue("@autor", libro.Autor);
                    command.Parameters.AddWithValue("@editorial", libro.Editorial);
                    command.Parameters.AddWithValue("@publicacion", libro.AnioPublicacion);
                    command.Parameters.AddWithValue("@genero", libro.Genero);
                    command.Parameters.AddWithValue("@disponibles", libro.CopiasDisponibles);
                    command.Parameters.AddWithValue("@totales", libro.CopiasTotales);
                    command.Parameters.AddWithValue("@id", libro.LibroID);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public List<Usuario> ObtenerLectores() {
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand("SELECT * FROM Usuarios WHERE rol='Lector';", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Usuario usuario = new Usuario {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Rol = reader.GetString(3),
                                FechaIngreso = reader.GetDateTime(4)
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listaUsuarios;
        }

        public List<Prestamos> ObtenerPrestamos() {
            List<Prestamos> listaPrestamos = new List<Prestamos>();

            using (var connection = GetConnection()) {
                connection.Open();
                using (var comandDB = new MySqlCommand($"SELECT T.TransaccionID, U.Nombre AS NombreUsuario, U.Apellido AS ApellidoUsuario, L.Titulo AS TituloLibro,  T.FechaPrestamo, T.FechaDevolucion, T.Estado FROM Transacciones T LEFT JOIN Usuarios U ON T.UsuarioID = U.UsuarioID LEFT JOIN Libros L ON T.LibroID = L.LibroID; ", connection)) {
                    comandDB.CommandType = System.Data.CommandType.Text;
                    comandDB.CommandTimeout = 60;

                    using (MySqlDataReader reader = comandDB.ExecuteReader()) {
                        while (reader.Read()) {
                            Prestamos prestamo = new Prestamos {
                                TransaccionID = reader.GetInt32(0),
                                nombreUsuario = reader.GetString(1),
                                apellidoUsuario = reader.GetString(2),
                                tituloLibro = reader.GetString(3),
                                FechaPrestamo = reader.GetDateTime(4).ToString("yyyy-MM-dd"),
                                FechaDevolucion = !reader.IsDBNull(5) ? reader.GetDateTime(5).ToString("yyyy-MM-dd") : null,
                                Estado = reader.GetString(6)
                            };

                            listaPrestamos.Add(prestamo);
                        }
                    }
                }
            }

            return listaPrestamos;
        }

        public bool insertarPrestamo(int usuario, int libro, string entrega, string devolucion) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Transacciones (UsuarioID, LibroID, FechaPrestamo, FechaDevolucion, Estado) VALUES (@usuario, @libro, @entrega, NULL, 'Prestado')", connection)) {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@libro", libro);
                    command.Parameters.AddWithValue("@entrega", entrega);
                    command.Parameters.AddWithValue("@devolucion", devolucion);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }

        public bool EditarPrestamo(int id) {
            bool exito = false;

            using (var connection = GetConnection()) {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Transacciones SET Estado = 'Devuelto', FechaDevolucion = CURDATE() WHERE TransaccionID = @id; ", connection)) {
                    command.Parameters.AddWithValue("@id", id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    exito = filasAfectadas > 0;
                }
            }

            return exito;
        }
    }
}
