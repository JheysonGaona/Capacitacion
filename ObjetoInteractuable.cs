using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(Rigidbody))]

    public class ObjetoInteractuable : Objeto {

        [SerializeField] private float alturaInteracción;
        [SerializeField] [Range(0, 360)] private float anguloRotación;
        [SerializeField] private bool puedeRotar;

        private Rigidbody componenteFisica;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes
        protected virtual void Awake(){
            componenteFisica = GetComponent<Rigidbody>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            ConfigurarRecursoFisica();
        }

        // Método que permite configurar el componente de fisicas
        private void ConfigurarRecursoFisica(){
            componenteFisica.useGravity = true;
            componenteFisica.isKinematic = false;
        }
    }
}