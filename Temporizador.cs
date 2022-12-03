using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Capacitacion {

    public class Temporizador : MonoBehaviour {

        [Tooltip("Texto que forma parte de la UI - se utiliza para mostrar el tiempo transcurrido")]
        [SerializeField] private TMP_Text textoTemporizador;

        private float tiempoPorFrame = 0f;
        private float tiempoPorSegundo = 0f;
        private float lineaDeTiempo = 1;
        private bool iniciar = false;

        private void Awake() {
            if(textoTemporizador != null){
                Debug.Log("No se ha instanciado un objeto de tipo TEXT correspondiente a la UI");
            }
        }

        private void Strat(){
            textoTemporizador.text = "00:00:00 seg";
        }

        // Método de llama de Unity, permite activar los recursos del script cuando la cámara renderiza el objeto
        private void OnBecameVisible() {
            enabled = true;
        }

        // Método de llama de Unity, permite desactivar los recursos del script cuando la cámara no renderiza el objeto
        private void OnBecameInvisible() {
            enabled = false;
        }

        // Método de llamada de Unity, se inicia el temporizador
        private void Update(){
            if(iniciar){
                tiempoPorFrame = Time.deltaTime * lineaDeTiempo;
                tiempoPorSegundo += tiempoPorFrame;
                UpdateClock(tiempoPorSegundo);
            }
        }

        // método empleado para actualizar el tiempo
        private void UpdateClock(float tiemInSeconds){
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
        }

        public bool Iniciar { set => iniciar = value; get => iniciar; }
    }
}