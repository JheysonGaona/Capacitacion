using UnityEngine;
namespace Capacitacion {

    [RequireComponent(typeof(Animator))]

    public class Puerta : ObjetoDinamico {

        [SerializeField] private bool estadoPuerta = false;
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

        public override void ActivarFuncionalidad() {
            estadoPuerta = !estadoPuerta;
            animPuerta.SetBool(nombreEstado, estadoPuerta);
            ActivarSonido();
        }

        public override void ResetearFuncionalidad() {
            if(animPuerta.GetBool(nombreEstado)){
                animPuerta.SetBool(nombreEstado, false);
            }
        }
    }
}