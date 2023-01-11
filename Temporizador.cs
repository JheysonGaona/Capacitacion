using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Capacitacion {

    public class Temporizador : MonoBehaviour {

        [Tooltip("Texto que forma parte de la UI - se utiliza para mostrar el tiempo transcurrido")]
        [SerializeField] private TMP_Text textoTemporizador;

        [Tooltip("Cual es el tiempo límite sobre el cual debe detenerse el cronómetro en horas")]
        [SerializeField] private int limiteTemporizadorHoras;

        [Tooltip("Cual es el tiempo límite sobre el cual debe detenerse el cronómetro en minutos")]
        [SerializeField] private int limiteTemporizadorMinutos;

        [Tooltip("Cual es el tiempo límite sobre el cual debe detenerse el cronómetro en segundos")]
        [SerializeField] private int limiteTemporizadorSegundos;

        [SerializeField] private Button btnAumentarTiempo;
        [SerializeField] private Button btnReducirTiempo;


        private float tiempoPorFrame = 0f;
        private float tiempoPorSegundo = 0f;
        private float lineaDeTiempo = 1;
        private bool iniciar = false;
        private int contador = 1;

        // Método de llamada de Unity, se llama una vez al iniciar el aplicativo, se configura las funciones del script
        private void Start(){
            if(!textoTemporizador){
                Debug.LogWarning("No se ha instanciado un objeto de tipo TEXT correspondiente a la UI");
            }
            textoTemporizador.text = "00:00:00 seg.";
            btnReducirTiempo.onClick.AddListener( () => DisminuirLineaTiempo() );
            btnAumentarTiempo.onClick.AddListener( () => AumentarLineaTiempo() );
            ValidarEstadoBotones();
        }

        // Método de llamada de Unity, el temporizador una vez inicializado se actualiza en cada frame del computador 
        private void Update(){
            if(iniciar){
                tiempoPorFrame = Time.deltaTime * lineaDeTiempo;
                tiempoPorSegundo += tiempoPorFrame;
                ActualizarReloj(tiempoPorSegundo);
            }
        }

        // Método empleado para actualizar el tiempo en horas, minutos y segundos
        private void ActualizarReloj(float tiemInSeconds){
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            if(tiemInSeconds < 0){
                tiemInSeconds = 0;
            }
            hours = (int) tiemInSeconds / 3600;
            minutes = (int) (tiemInSeconds - (hours * 3600)) / 60;
            // minutes = (int) tiemInSeconds/ 60;
            seconds = (int) tiemInSeconds % 60;
            textoTemporizador.text = string.Format("{0:00}:{1:00}:{2:00} seg.", hours, minutes, seconds);

            // Se valida si el tiempo del temporizador, si este coincide con el tiempo establecido dentro de la práctica, se detendra
            if(hours >= limiteTemporizadorHoras && minutes >= limiteTemporizadorMinutos && seconds >= limiteTemporizadorSegundos){
                iniciar = false;
                ValidarEstadoBotones();
            }
        }

        // Método que permite reiniciar el cronometro
        public void ReiniciarReloj(){
            tiempoPorFrame = 0f;
            tiempoPorSegundo = 0f;
            lineaDeTiempo = 1;
            iniciar = false;
            contador = 1;
            textoTemporizador.text = "00:00:00 seg.";
            ValidarEstadoBotones();
        }

        // Método empleado aumentar el flujo de tiempo del cronometro
        public void AumentarLineaTiempo(){
            contador++;
            ValidarContadorBotones();
            if(contador <= 9) lineaDeTiempo *= 2;
        }

        // Método empleado reducir el flujo de tiempo del cronometro
        public void DisminuirLineaTiempo(){
            contador--;
            ValidarContadorBotones();
            if(contador != 1) lineaDeTiempo /= 2;
        }

        // Método que permite validar la interaccion de los botones
        private void ValidarContadorBotones(){
            btnReducirTiempo.interactable = contador == 1 ? false: true;
            btnAumentarTiempo.interactable = contador <= 9 ? true: false;
        }

        private void ValidarEstadoBotones(bool condicion = false){
            if(condicion){
                ValidarContadorBotones();
            }else{
                btnReducirTiempo.interactable = condicion;
                btnAumentarTiempo.interactable = condicion;
            }
        }

        // Getters & setters
        public bool Iniciar {
            get { return iniciar; }
            set {
                iniciar = value;
                ValidarEstadoBotones(iniciar);
            }
        }
    }
}