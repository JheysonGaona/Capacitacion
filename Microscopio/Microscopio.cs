using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Capacitacion {

    public class Microscopio : MonoBehaviour {
        
        // Variables del script
        [Header("Componentes del microscopio")]
        [SerializeField] private GameObject ocular;
        [SerializeField] private GameObject baseRotarRevolver;
        [SerializeField] private GameObject platina;
        [SerializeField] private GameObject zonaMuestra;
        [SerializeField] private GameObject foco;
        [SerializeField] private GameObject baseRotarTornilloMacrometrico;
        [SerializeField] private GameObject baseRotarTornilloMicrometrico;
        [SerializeField] private GameObject cimiento;
        [SerializeField] private GameObject brazo;

        [Header("Componentes del monitor")]
        [SerializeField] private GameObject monitorPc;
        [SerializeField] private Image imgZoomMicroscopio;
        [SerializeField] private TMP_Text textoLenteObjetivo;

        [Header("Movimiento platina")]
        [SerializeField] private Transform posicionMaxPlatina;
        [SerializeField] private Transform posicionMinPlatina;

        [Header("Movimiento camara microscopio")]
        [SerializeField] private Transform camaraMicroscopio;
        [SerializeField] private Transform posicionMinCamara;
        [SerializeField] private Transform posicionMaxCamara;

        private ColorGrading m_ColorAdjustments;
        public PostProcessVolume boxVolume;

        // Posicion actual y puntos de movimiento de la platina
        private Vector3 posicionInicialplatina;
        private Vector3 minPosition_Platen;
        private Vector3 maxPosition_Platen;

        // Posicion actual y puntos de movimiento de la camara
        private Vector3 posicionInicialCamara;
        private Vector3 minPosition_Camera;
        private Vector3 maxPosition_Camera;

        private Light luzFoco;
        private Muestra muestra;
        private Animator animMonitorPc;
        private LectorAnguloBisagra lectorTornilloMacro;
        private LectorAnguloBisagra lectorTornilloMicro;

        private Transform moverPlatina;

        private bool estadoLuzFoco = false;
        private bool muestraColocada = false;
        private float tiempoColoarMuestra = 1f;
        private int lenteObjetivo = 0;

        private void Awake(){
            luzFoco = foco.GetComponentInChildren<Light>();            
            animMonitorPc = monitorPc.GetComponent<Animator>();
            lectorTornilloMicro = baseRotarTornilloMicrometrico.GetComponent<LectorAnguloBisagra>();
            lectorTornilloMacro = baseRotarTornilloMacrometrico.GetComponent<LectorAnguloBisagra>();
            // Inicialización del post-procesamiento
            m_ColorAdjustments = boxVolume.profile.GetSetting<ColorGrading>();
        }

        private void Start(){
            luzFoco.enabled = false;
            // Datos de la platina
            posicionInicialplatina = platina.transform.localPosition;
            minPosition_Platen = posicionMinPlatina.localPosition;
            maxPosition_Platen = posicionMaxPlatina.localPosition;
            
            // Datos de la camara
            posicionInicialCamara = camaraMicroscopio.localPosition;
            minPosition_Camera = posicionMinCamara.localPosition;
            maxPosition_Camera = posicionMaxCamara.localPosition;
        }

        private void Update(){
            // Devuelve un valor de rango entre [-1, 1] del tornillo macro y micro
            // float valorTornilloMicro = lectorTornilloMicro.ObtenerValorRotacion();
            float valorTornilloMacro = lectorTornilloMacro.ObtenerValorRotacion();
            if(valorTornilloMacro >= 0){
                platina.transform.localPosition = Vector3.Lerp(posicionInicialplatina, maxPosition_Platen, valorTornilloMacro);
                camaraMicroscopio.localPosition = Vector3.Lerp(posicionInicialCamara, minPosition_Camera, valorTornilloMacro);
            }else if(valorTornilloMacro <= 0){
                platina.transform.localPosition = Vector3.Lerp(posicionInicialplatina, minPosition_Platen, -valorTornilloMacro);
                camaraMicroscopio.localPosition = Vector3.Lerp(posicionInicialCamara, maxPosition_Camera, -valorTornilloMacro);
            }

            float valorTornilloMicro = lectorTornilloMicro.ObtenerValorRotacion();
            if(valorTornilloMicro >= 0){

            }else if(valorTornilloMicro <= 0){

            }
        }

        public void AjusteLuz(float valor){
            m_ColorAdjustments.postExposure.value = valor;
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
            // Activar vista de muestraa
            EstablecerVistaMonitor();
        }

        private void OnBecameInvisible() {
            enabled = false;
            Debug.Log("Invisible");
        }

        private void OnBecameVisible() {
            enabled = true;
            Debug.Log("Visible");
        }

        public bool MuestraColocada { set => muestraColocada = value; get => muestraColocada; }
    }
}