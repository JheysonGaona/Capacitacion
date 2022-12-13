using UnityEngine;
using UnityEngine.Events;

namespace Capacitacion {

    public class Interruptor : ObjetoDinamico {
        
        // Variables de la clase
        [SerializeField] private Color colorApagado;
        [SerializeField] private Color colorEncendido;
        public UnityEvent evento;

        private bool activarBoton = false;
        private Color colorOriginal;
        private Material materialBase;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, más la herencia
        new protected virtual void Awake() {
            base.Awake();
            materialBase = GetComponent<Renderer>().material;
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto y se instancian las variables
        new private void Start() {
            base.Start();
            colorOriginal = materialBase.color;
            materialBase.color = colorApagado;
        }

        // Método abtracto implementado que permite activar la funcionalidad del objeto
        public override void ActivarFuncionalidad() {
            evento.Invoke();
            activarBoton = !activarBoton;
            materialBase.color = activarBoton ? colorEncendido: colorApagado;
            ActivarSonido();
        }

        // Método que permite resetear los parametros del mechero
        public override void ResetearFuncionalidad(){
            if(activarBoton){
                ActivarFuncionalidad();
            }
        }

        // Método de unity, se llama cuando se desabilita el script
        private void OnDisable() {
            materialBase.color = colorOriginal;
        }
    }
}