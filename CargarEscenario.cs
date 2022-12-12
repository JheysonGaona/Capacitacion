using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Capacitacion {

    public class CargarEscenario : MonoBehaviour {
        
        [Header("Parámetros para cargar escenario")]
        [SerializeField] private int indiceEscena;
        [SerializeField] private bool escenaCargada = false;

        [Header("Parámetros de pantalla de carga")]
        [SerializeField] private GameObject pantallaCarga;
        [SerializeField] private Image barraCarga;


        // Método que permite cargar un escenario único de manera directa, pero se ve afectada por la siguiente escena a cargar,
        //  es decir, hasta que la escena a cargar no este completa, la primera escena se cuelga o congela
        public void CargarEscenarioUnico(){
            SceneManager.LoadScene(indiceEscena, LoadSceneMode.Single);
        }

        // Permite cargar una escena en segundo plano, sin detener la ejecución del proyecto.
        // Se emplea pantalla de carga, para anular el proceso de congelación de pantalla
        private IEnumerator CargarEscenarioAsincrono(){
            pantallaCarga.SetActive(true);
            AsyncOperation cargando = SceneManager.LoadSceneAsync(indiceEscena, LoadSceneMode.Single);
            while(!cargando.isDone){
                barraCarga.fillAmount = cargando.progress;
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(escenaCargada){
                escenaCargada = false;
                SceneManager.UnloadSceneAsync(indiceEscena);
            }else{
                escenaCargada = true;
                SceneManager.LoadSceneAsync(indiceEscena, LoadSceneMode.Additive);
            }
        }
    }
}