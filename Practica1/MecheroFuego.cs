using UnityEngine;

namespace Capacitacion {
    public class MecheroFuego : ObjetoDinamico {

        //Variables de la clase
        [Tooltip("Se debe incluir el efecto de particulas correspondiente al objeto, para este caso fuego")]
        [SerializeField] private ParticleSystem efectoFuego;
        [SerializeField] public Vector3 limiteSuperior;

        private BoxCollider disparadorCajaColision;
        private Vector3 limiteInferior;
        private bool estaEncendido = false;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, por consiguiente hereda
        new protected virtual void Awake() {
            base.Awake();
            disparadorCajaColision = efectoFuego.gameObject.GetComponent<BoxCollider>();
        }
        
        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            efectoFuego.gameObject.name = "EfectoFuego";
            limiteInferior = disparadorCajaColision.center;
            disparadorCajaColision.isTrigger = true;
        }

        // Método que permite activar la funcionalidad del objeto
        public override void ActivarFuncionalidad(){
            estaEncendido = !estaEncendido;
            if(estaEncendido){
                ActivarSonido();
                efectoFuego.Play();
                disparadorCajaColision.center = Vector3.MoveTowards(disparadorCajaColision.center, limiteSuperior, 1f);
            }else{
                DesactivarFuncionalidad();
            }
        }

        // Método que permite resetear los parametros del mechero
        public override void ResetearFuncionalidad(){
            if(estaEncendido){
                estaEncendido = false;
                DesactivarFuncionalidad();
            }
        }

        // Método que permite descativar los recursos del mechero
        private void DesactivarFuncionalidad(){
            DesactivarSonido();
            efectoFuego.Stop();
            disparadorCajaColision.center = Vector3.MoveTowards(disparadorCajaColision.center, limiteInferior, 1f);
        }
    }
}