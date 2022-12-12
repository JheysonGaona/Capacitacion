using System.Collections;
using UnityEngine;

namespace Capacitacion {

    public class P1_CompuestosOrganicos : MonoBehaviour {

        [Header("Script de matraz Oceano y Atmosfera")]
        [SerializeField] private MatrazOceano matrazOceano;
        [SerializeField] private MatrazAtmosfera matrazAtmosfera;
        
        [Tooltip("Tiempo que debe transcurrir antes de que el fuego evapore el agua")]
        [SerializeField] private float tiempoEvaporacionAgua;

        [Tooltip("Tiempo que debe transcurrir antes de que el vapor del agua, gases y electricidad generen gotas de agua")]
        [SerializeField] private float tiempoReaccionQuimicaGases;

        [Tooltip("Tiempo que debe transcurrir antes de que el agua cambie de color")]
        [SerializeField] private float tiempoCambioColorAgua;

        [Header("Efectos de particulas")]
        [Tooltip("Se debe incluir el efecto de particulas correspondiente al objeto, para este caso vapor de agua")]
        [SerializeField] private ParticleSystem efectoVaporAgua;

        [Tooltip("Se debe incluir el efecto de particulas correspondiente al objeto, para este caso gases")]
        [SerializeField] private ParticleSystem[] efectoGases;

        [Tooltip("Se debe incluir el efecto de particulas correspondiente al objeto, para este caso electricidad")]
        [SerializeField] private ParticleSystem efectoElectricidad;

        [Tooltip("Se debe incluir el efecto de particulas correspondiente al objeto, para este caso gotas de agua")]
        [SerializeField] private ParticleSystem efectoGotasAgua;

        [Header("Efectos de sonido")]
        
        [Tooltip("Se debe incluir el efecto de sonido para el agua cuando se evapora")]
        [SerializeField] private AudioClip efectoSonidoAguaHirviendo;

        [Tooltip("Se debe incluir el efecto de sonido para el activador de electricdad")]
        [SerializeField] private AudioClip efectoSonidoElectricidad;

        [Header("Panel Etiquetas")]
        [Tooltip("Se debe incluir el panel de etiquetas para activar o desactivar")]
        [SerializeField] private GameObject pnlEtiquetas;

        private bool estadoPnlEtiquetas = true;

        // Método de llamada de Unity, se inicializan los componentes de las matraces
        private void Awake(){
            ConfiguracionMatrazOceano();
            ConfiguracionMatrazAtmosfera();
        }

        // Método que permite configurar la matraz oceano
        private void ConfiguracionMatrazOceano(){
            matrazOceano.RecursoSonido.clip = efectoSonidoAguaHirviendo;
            matrazOceano.TiempoColorAgua = tiempoCambioColorAgua;
            matrazOceano.TiempoEvaporacion = tiempoEvaporacionAgua;
            matrazOceano.EfectoVaporAgua = efectoVaporAgua;
        }

        // Método que permite configurar la matraz atmosfera
        private void ConfiguracionMatrazAtmosfera(){
            matrazAtmosfera.RecursoSonido.clip = efectoSonidoElectricidad;
            matrazAtmosfera.TiempoReaccion = tiempoReaccionQuimicaGases;
            matrazAtmosfera.EfectoGases = efectoGases;
            matrazAtmosfera.EfectoElectricidad = efectoElectricidad;
            matrazAtmosfera.EfectoGotasAgua = efectoGotasAgua;
            matrazAtmosfera.MatrazOceano = matrazOceano;
        }

        public void ActivarEtiquetas(){
            estadoPnlEtiquetas = !estadoPnlEtiquetas;
            pnlEtiquetas.SetActive(estadoPnlEtiquetas);
        }

        // Método que permite reiniciar la práctica
        public void ReiniciarPractica(){
            matrazOceano.ResetearParametros();
            matrazAtmosfera.ResetearParametros();
        }
    }
}