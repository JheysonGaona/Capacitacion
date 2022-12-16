using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class ObjetoRotatorio : Objeto {
        
        [SerializeField] private float velocidadRotación = 10f;
        [SerializeField] private float tiempoRotacion = 1.5f;

        private IEnumerator RotarObjeto(){
            float tiempoTranscurrido = 0;
            while(tiempoTranscurrido < tiempoRotacion){
                this.transform.RotateAround(transform.position, transform.forward, velocidadRotación * Time.deltaTime);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }
        }

        public override void ActivarFuncionalidad() {
            StartCoroutine(RotarObjeto());
        }

        public override void ResetearFuncionalidad()
        {
            throw new System.NotImplementedException();
        }
    }
}