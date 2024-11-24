using System;
using System.Collections.Generic;
using System.Text;

namespace Cache {
    public class Prestamos {
        public int TransaccionID { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string tituloLibro { get; set; }
        public string FechaPrestamo { get; set; }
        public string FechaDevolucion { get; set; }
        public string Estado { get; set; }
    }
}
