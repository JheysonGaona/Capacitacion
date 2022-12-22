using System.Collections;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(AudioSource))]

    public class MatrazOceano : MonoBehaviour {
        
        // Variables de la clase
        [Tooltip("Animaciones que permiten que el color del agua se modifique")]
        public Animator[] controladorAnimacionOceano;

        private float tiempoEvaporacion;
        private float tiempoColorAgua;
        private float tiempoColorAguaOriginal;
        private float tiempoEvaporacionOriginal;
        private bool activarEvaporacion;
        private bool activarCambioColorAgua;
        private bool activarTemporizador;

        private AudioSource recursoSonido;
        private ParticleSystem efectoVaporAgua;
 
        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se carga el recurso de sonido
        private void Awake() {
            recursoSonido = GetComponent<AudioSource>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        private void Start() {
            tiempoColorAguaOriginal = tiempoColorAgua;
            tiempoEvaporacionOriginal = tiempoEvaporacion;
            recursoSonido.loop = true;
            recursoSonido.playOnAwake = false;
            enabled = false;
        }

        // Método de Unity, permite verificar las colisiones de entrada que tiene contacto con el objeto
        private void OnTriggerEnter(Collider other) {
            if(other.name.Equals("EfectoFuego") && !activarEvaporacion){
                InvokeRepeating(nameof(TemporizadorFuego), 1, 1);
            }
        }

        // Método de Unity, permite verificar las colisiones de salida que tiene contacto con el objeto
        private void OnTriggerExit(Collider other) {
            if(other.name.Equals("EfectoFuego") && !activarEvaporacion){
                CancelInvoke("TemporizadorFuego");
                tiempoEvaporacion = tiempoEvaporacionOriginal;
            }
        }

        // Método que permite simular un temporizador para que se produzca la evaporación
        private void TemporizadorFuego() {
            tiempoEvaporacion--;
            if (tiempoEvaporacion <= 0) {
                CancelInvoke("TemporizadorFuego");
                efectoVaporAgua.Play();
                recursoSonido.Play();
                activarEvaporacion = true;
            }
        }

        // Método que permite simular un temporizador para que se produzca el cambio de color del agua
        private void TemporizadorColorAgua() {
            tiempoColorAgua--;
            if (tiempoColorAgua <= 0) {
                CancelInvoke("TemporizadorColorAgua");
                activarCambioColorAgua = true;
                foreach(Animator current in controladorAnimacionOceano){
                    current.SetBool("cambiarColor", true);
                }
                // Enviar a P1 la práctica ha finalizado
            }
        }

        // Método que permite resetear los parametros de la práctica
        public void ResetearParametros(){
            tiempoEvaporacion = tiempoEvaporacionOriginal;
            tiempoColorAgua = tiempoColorAguaOriginal;
            if(activarEvaporacion){
                efectoVaporAgua.Stop();
                recursoSonido.Stop();
                activarEvaporacion = false;
                activarCambioColorAgua = false;
                foreach(Animator current in controladorAnimacionOceano){
                    current.SetBool("cambiarColor", false);
                }
            }
        }

        // Método que permite validar si los componentes quimicos: vapor, gases y electricidad se encuentran habilitados
        public bool ValidarReaccion(bool activarGas, bool activarElectricidad){
            activarCambioColorAgua = activarEvaporacion && activarGas && activarElectricidad ? true: false;
            return activarCambioColorAgua;
        }

        // Método que permite cambiar el color del agua cuando el experimento ha finalizado con exito
        public void ColorAgua(bool activarReaccion){
            if(activarReaccion & activarCambioColorAgua){
                activarTemporizador = true;
                InvokeRepeating(nameof(TemporizadorColorAgua), 1, 1);
            }else{
                if(activarTemporizador){
                    activarTemporizador = false;
                    CancelInvoke("TemporizadorColorAgua");
                    tiempoColorAgua = tiempoColorAguaOriginal;
                }
            }
        }

        private void OnBecameVisible() {
            enabled = true;
        }

        private void OnBecameInvisible() {
            enabled = false;
        }

        // Getters & Setters de la clase
        public float TiempoEvaporacion { set => tiempoEvaporacion = value; get => tiempoEvaporacion; }
        public float TiempoColorAgua { set => tiempoColorAgua = value; get => tiempoColorAgua; }
        public ParticleSystem EfectoVaporAgua { set => efectoVaporAgua = value; }
        public AudioSource RecursoSonido { get => recursoSonido; }
    }
}