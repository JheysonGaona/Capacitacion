using UnityEngine;

namespace Capacitacion {
    public class Libro : ObjetoDinamico {
     
        // Variables de la clase
        [SerializeField] private Instrucciones instrucciones;
        [SerializeField] private Sprite[] imagenesIntruccion;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, más la herencia
        new protected virtual void Awake() {
            base.Awake();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto y se instancian las variables
        new private void Start() {
            base.Start();
        }

        // Método abtracto implementado que permite activar la funcionalidad del objeto
        public override void ActivarFuncionalidad() {
            ActivarSonido();
            instrucciones.EstablecerInstruciones(imagenesIntruccion);
        }

        // Método que permite resetear los parametros del mechero
        public override void ResetearFuncionalidad(){
            ActivarFuncionalidad();
        }
    }
}