using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        [SerializeField] private TMP_Text textoLenteObjetivo;

        private Light luzFoco;
        private Muestra muestra;
        private Animator animMonitorPc;

        private bool estadoLuzFoco = false;
        private bool muestraColocada = false;
        private float tiempoColoarMuestra = 1f;
        private int lenteObjetivo = 0;

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

        public bool ColocarPortaobjetos(Muestra muestra){
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

        public void CambiarLenteObjetivo(int idLente, string resolucion){
            lenteObjetivo = idLente;
            textoLenteObjetivo.text = resolucion;
            if(muestra != null) EstablecerVistaMonitor();
        }

        private void EstablecerVistaMonitor(){
            imgZoomMicroscopio.sprite = lenteObjetivo >= 0 ? muestra.ListaImagenesMicroscopioZoom[lenteObjetivo]: null;
        }

        private void RetirarMuestra(){
            muestra.transform.SetParent(null);
            imgZoomMicroscopio.sprite = null;
            this.muestra = null;
        }

        private IEnumerator EstablecerMuestra(){
            // Configurar muestra
            muestra.transform.SetParent(zonaMuestra.transform);
            yield return new WaitForSeconds(tiempoColoarMuestra);
            // Activar vista de muestra
            EstablecerVistaMonitor();
        }

        public bool MuestraColocada { set => muestraColocada = value; get => muestraColocada; }
    }
}