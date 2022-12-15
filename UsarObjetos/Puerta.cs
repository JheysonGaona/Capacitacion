using UnityEngine;
namespace Capacitacion {

    [RequireComponent(typeof(Animator))]

    public class Puerta : ObjetoDinamico {

        // variables de la clase
        [Tooltip("Se utiliza para determinar y saber el estado de la puerta, false => cerrado, true => abierta")]
        [SerializeField] private bool estadoPuerta = false;

        [Tooltip("Nombre de la condicion del animator, que efectua la acción de la puerta")]
        [SerializeField] private string nombreEstado = "activate";
        
        private Animator animPuerta;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, por consiguiente hereda
        new protected virtual void Awake() {
            base.Awake();
            animPuerta = gameObject.GetComponent<Animator>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
        }

        // Método que permite activar la funcionalidad del objeto
        public override void ActivarFuncionalidad() {
            base.ActivarFuncionalidad();
            estadoPuerta = !estadoPuerta;
            animPuerta.SetBool(nombreEstado, estadoPuerta);
        }

        // Método que permite resetear la funcionalidad de la puerta
        public override void ResetearFuncionalidad() {
            if(animPuerta.GetBool(nombreEstado)){
                animPuerta.SetBool(nombreEstado, false);
            }
        }
    }
}