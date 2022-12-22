using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class Foco : ObjetoDinamico, IActivarObjeto {

        // Variables de la clase
        [SerializeField] private Microscopio microscopio;
        [SerializeField] private int limiteIntensidadLuz = 3;
        [SerializeField] private float intervaloLuz = 0.375f;

        private float contadorIntensidad = 0;

        // Si es verdad rota hacia la derecha, si es falso rota a la izquierda
        private bool limiteAjustePermitido = true;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, más la herencia
        new protected virtual void Awake() {
            base.Awake();
            if(microscopio == null) Debug.LogWarning("Error, el foco del microscopio no se ha instanciado una clase Microscopio"); 
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto y se instancian las variables
        new private void Start() {
            base.Start();
        }

        private bool ValidarLimiteIntensidadLuz(){
            return limiteAjustePermitido = contadorIntensidad >= -limiteIntensidadLuz && contadorIntensidad <= limiteIntensidadLuz ? true: false;
        }

        // Método abtracto implementado que permite activar la funcionalidad del objeto
        public override void ActivarFuncionalidad() {
            base.ActivarFuncionalidad();
            contadorIntensidad = contadorIntensidad + intervaloLuz;
            microscopio.AjusteLuz(contadorIntensidad);
            if(!ValidarLimiteIntensidadLuz()) contadorIntensidad = limiteIntensidadLuz;
        }

        public void ActivarSegundaFuncionalidad() {
            contadorIntensidad = contadorIntensidad - intervaloLuz;
            microscopio.AjusteLuz(contadorIntensidad);
            if(!ValidarLimiteIntensidadLuz()) contadorIntensidad = -limiteIntensidadLuz;
        }

        public bool activarFuncionalidad { get => limiteAjustePermitido; set => limiteAjustePermitido = value; }
    }
}