using UnityEngine;
using System;

namespace Capacitacion {

    // Variables de la clase
    [Serializable]
    public class Pregunta {
        public int Id;
        public string TextoPregunta;
        public Respuesta [] Respuestas;
    }


    [Serializable]
    public class Respuesta {
        public string TextoRespuesta;
        public bool ValorRespuesta;
    }
}