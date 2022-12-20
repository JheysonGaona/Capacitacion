using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class ObjetoRotatorio : Objeto, IActivarObjeto {
        
        [SerializeField] private float anguloRotacion = 120f;
        [SerializeField] private float tiempoRotacion = 1f;
        [SerializeField] private bool tieneLimiteRotacion = false;
        [SerializeField] [Range(0, 180)] private int limiteMaxRotacion;
        [SerializeField] [Range(-180, 0)] private int limiteMinRotacion;

        private Transform datosInicial;
        private bool rotando = false;

        // Si es verdad rota hacia la derecha, si es falso rota a la izquierda
        private bool ladoRotacion = false;
        public bool activarFuncionalidad { get => ladoRotacion; set => ladoRotacion = value; }
        
        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            datosInicial = this.transform;
        }

        private IEnumerator RotarObjeto(){
            rotando = true;
            float tiempoTranscurrido = 0;
            while(tiempoTranscurrido <= tiempoRotacion){
                this.transform.position = datosInicial.position;
                this.transform.RotateAround(transform.position, transform.forward, (activarFuncionalidad ? anguloRotacion: -anguloRotacion) * Time.deltaTime);
                tiempoTranscurrido += Time.deltaTime;

                yield return null;
            }
            // int rotationZ = (int) this.transform.eulerAngles.z;
            // this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y, rotationZ);
            rotando = false;
        }

        public override void ActivarFuncionalidad() {
            activarFuncionalidad = true;
            if(!rotando) StartCoroutine(RotarObjeto());
        }

        public void ActivarSegundaFuncionalidad() {
            activarFuncionalidad = false;
            if(!rotando) StartCoroutine(RotarObjeto());
        }

        public override void ResetearFuncionalidad() {
            this.transform.position = datosInicial.position;
        }

        public override void EstablecerTipoObjeto() {
            this.caracteristicaObjeto = tipoObjeto.Estatico;
        }
    }
}