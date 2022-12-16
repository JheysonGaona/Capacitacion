using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Capacitacion {

    public class Microscopio : MonoBehaviour {
        
        // Variables del script
        [Header("Componentes del microscopio")]
        [SerializeField] private GameObject ocular;
        [SerializeField] private GameObject revolver;
        [SerializeField] private GameObject platina;
        [SerializeField] private GameObject zonaMuestra;
        [SerializeField] private GameObject foco;
        [SerializeField] private GameObject tornilloMacrometrico;
        [SerializeField] private GameObject tornilloMicrometrico;
        [SerializeField] private GameObject cimiento;
        [SerializeField] private GameObject brazo;

        [Header("Componentes del monitor")]
        [SerializeField] private GameObject monitorPc;

        [Header("Componentes del monitor")]
        [SerializeField] private Image imgZoomMicroscopio;

        private Light luzFoco;
        private Muestra muestra;
        private Animator animMonitorPc;

        private float tiempoColoarMuestra = 1f;
        private bool estadoLuzFoco = false;
        private bool muestraColocada = false;

        private void Awake(){
            luzFoco = foco.GetComponentInChildren<Light>();            
            animMonitorPc = monitorPc.GetComponent<Animator>();
        }

        private void Start(){
            luzFoco.enabled = false;
        }

        public void InterruporMicroscopio(){
            estadoLuzFoco = !estadoLuzFoco;
            luzFoco.enabled = estadoLuzFoco;
            animMonitorPc.SetBool("activate", estadoLuzFoco);
        }

        public bool ColocarMuestra(Muestra muestra){
            if(!muestraColocada){
                muestraColocada = true;
                this.muestra = muestra;
                StartCoroutine(EstablecerMuestra());
                return true;
            }else{
                if(this.muestra == muestra){
                    muestraColocada = false;
                    RetirarMuestra();
                    return true;
                }else{
                    return false;
                }
            }
        }

        private void RetirarMuestra(){
            muestra.transform.SetParent(null);
            this.muestra = null;
        }

        private IEnumerator EstablecerMuestra(){
            // Configurar muestra
            muestra.transform.SetParent(zonaMuestra.transform);
            yield return new WaitForSeconds(tiempoColoarMuestra);

            // Activar vista de muestra
        }


        public bool MuestraColocada { set => muestraColocada = value; get => muestraColocada; }
    }
}