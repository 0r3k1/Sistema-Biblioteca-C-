using System;
using MySql.Data.MySqlClient;
using capaDatos;
using System.Data;
using System.Collections.Generic;

using Cache;

namespace capaDominio {
    public class Domain {

        public static bool InsertarLogin(int id, string usuario, string pass) {
            Conn conn = new Conn();
            return conn.InsertarLogin(id, usuario, pass);
        }

        public static bool modificarLogin(int id, string usuario, string pass) {
            Conn conn = new Conn();
            return conn.modificarLogin(id, usuario, pass);
        }

        public static Login validarLogin(string user, string pass) {
            Conn conn = new Conn();
            return conn.validarUsuario(user, pass);
        }

        public static List<Usuario> ObtenerUsuarios(){
            Conn conn = new Conn();
            return conn.ObtenerUsuarios();
            
        }

        public static Usuario ObtenerUsuario(int userID) {
            Conn conn = new Conn();
            return conn.ObtenerUsuario(userID);

        }

        public static int ObtenerIdUsuario(string Usuario, string Apellido) {
            Conn conn = new Conn();
            return conn.ObtenerUsuario(Usuario, Apellido).UsuarioID;

        }

        public static bool eliminarUsuario(int id) {
            Conn conn = new Conn();
            return conn.EliminarUsuario(id);
        }

        public static bool InsertarUsuario(string nombre, string apellido, string rol) {
            Conn conn = new Conn();
            return conn.InsertarUsuario(nombre, apellido, rol);
        }

        public static bool EditarUsuario(int id, string nombre, string apellido, string rol) {
            Conn conn = new Conn();
            return conn.EditarUsuario(id, nombre, apellido, rol);
        }

        public static List<Libro> obtenerLibros() {
            Conn conn = new Conn();

            return conn.ObtenerLibros();
        }

        public static Libro obtenerLibro(int id) {
            Conn conn = new Conn();

            return conn.ObtenerLibro(id);
        }

        public static List<Libro> OrdenarLibros(string column, string direct = "asc") {
            Conn conn = new Conn();

            return conn.ordenarLibros(column, direct);
        }

        public static List<Libro> buscarLibros(string op, string buscar) {
            Conn conn = new Conn();

            return conn.buscarLibros(op, buscar);
        }

        public static bool eliminarLibro(int id) {
            Conn conn = new Conn();
            return conn.EliminarLibro(id);
        }

        public static bool InsertarLibro(Libro libro) {
            Conn conn = new Conn();
            return conn.InsertarLibro(libro);
        }

        public static bool editarLibro(Libro libro) {
            Conn conn = new Conn();
            return conn.EditarLibro(libro);
        }

        public static List<Usuario> ObtenerLectores() {
            Conn conn = new Conn();
            return conn.ObtenerLectores();
        }

        public static List<Prestamos> ObtenerPrestamos() {
            Conn conn = new Conn();
            return conn.ObtenerPrestamos();
        }

        public static bool insertarPrestamo(int usuario, int libro, string entrega, string devolucion) {
            Conn conn = new Conn();
            return conn.insertarPrestamo(usuario, libro, entrega, devolucion);
        }

        public static bool devolverLibro(int id) {
            Conn conn = new Conn();
            return conn.EditarPrestamo(id);
        }
    }
}
