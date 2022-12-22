using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class InterpolarObjeto : MonoBehaviour {

        [SerializeField] private float tiempoInterpolar;
        [SerializeField] private Transform puntoPartida;
        [SerializeField] private Transform puntoDestino;
        [SerializeField] private bool activar;

        private float tiempoTranscurrido;

        // Método de llamada de Unity, se llama en cada frame del PC
        // Se interpola el objeto adjunto a este script
        private void Update() {
            if(activar){
                tiempoTranscurrido += Time.deltaTime;
                float porcentaje = tiempoTranscurrido / tiempoInterpolar;
                this.transform.position = Vector3.Lerp(puntoPartida.position, puntoDestino.position, porcentaje);
                float distance = Vector3.Distance(this.transform.position, puntoDestino.position);
                if(distance <= 0.01f){
                    ResetearInterpolacion();
                }
            }
        }

        public void ResetearInterpolacion(){
            activar = !activar;
            tiempoTranscurrido = 0;
        } 
    }
}