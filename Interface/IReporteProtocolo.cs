using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion{

    public interface IReporteProtocolo {
        public bool UsaMandil();

        public bool UsaMascarilla();

        public bool UsaGuantes();

        public bool UsaGorra();
    }
}