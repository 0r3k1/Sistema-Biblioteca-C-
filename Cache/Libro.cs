using System;
using System.Collections.Generic;
using System.Text;

namespace Cache {
    public class Libro {
        public int LibroID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }
        public int CopiasDisponibles { get; set; }
        public int CopiasTotales { get; set; }
    }
}
