using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {
    public interface IActivarObjeto{

        public bool activarFuncionalidad { get; set; }

        public void ActivarSegundaFuncionalidad();
    }
}