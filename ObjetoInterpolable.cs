using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class ObjetoInterpolable : Objeto {
        
        // Variables de la clase
        [Tooltip("Lugar donde se colocara la muestra una vez que se presione clic sobre ella")]
        [SerializeField] private Transform puntoDestino;

        [Tooltip("Lugar donde se dara inicio la transición de colocar la muestra, cree un gameObject hijo dentro de la muestra y posterior a ello saquelo para y situelo sobre esta variable")]
        [SerializeField] private Transform puntoPartida;

        [Tooltip("En cuanto tiempo desea que se produsca la interpolación de la muestra desde el punto inicial y punto partida")]
        [SerializeField] [Range(1, 10)] private float tiempoTransicion = 3;
        
        private Transform puntoGuardado;
        private float tiempoTrasncurrido;
        private bool activarInterpolado = false;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, más la herencia
        protected virtual void Awake() {
            
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto y se instancian las variables
        new protected void Start() {
            base.Start();
            enabled = false;
            puntoGuardado = puntoPartida;
        }

        // Método de llamada de Unity, se llama en cada frame del PC
        // Se interpola el objeto adjunto a este script
        protected virtual void Update() {
            if(activarInterpolado){
                tiempoTrasncurrido += Time.deltaTime;
                float porcentaje = tiempoTrasncurrido / tiempoTransicion;
                this.transform.position = Vector3.Lerp(puntoPartida.position, puntoDestino.position, porcentaje);
                float distance = Vector3.Distance(this.transform.position, puntoDestino.position);
                if(distance <= 0.01f){
                   ResetearInterpolacion();
                }
            }
        }

        // Método que permite resetear la interpolación una vez que se complete el movimiento
        private void ResetearInterpolacion(){
            activarInterpolado = false;
            tiempoTrasncurrido = 0;
            puntoPartida = puntoDestino;
            puntoDestino = puntoGuardado;
            puntoGuardado = puntoPartida;
        }

        // Método asbtracto que se hereda, para activar la funcionalidad de este objeto
        public override void ActivarFuncionalidad() {
            activarInterpolado = true;
        }

        // Método que permite resetear los parametros del mechero
        public override void ResetearFuncionalidad(){

        }

        public override void EstablecerTipoObjeto() {
            this.caracteristicaObjeto = tipoObjeto.Estatico;
        }

        private void OnBecameInvisible() {
            enabled = false;
        }

        private void OnBecameVisible() {
            enabled = true;
        }
    }
}