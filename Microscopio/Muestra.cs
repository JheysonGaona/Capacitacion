using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class Muestra : ObjetoInterpolable {
        
        // Variables de la clase
        [Tooltip("Se debe selecionar el script del Microscopio que se ha situado sobre un objeto dentro de Unity")]
        [SerializeField] private Microscopio microscopio;

        [Tooltip("3 imágenes que se deben colocar para validar cada resolución de acercamiento sobre el microscopio")]
        [SerializeField] private Sprite[] listaImagenesMicroscopioZoom;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, más la herencia
        new private void Awake() {
            base.Awake();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto y se instancian las variables
        new private void Start() {
            base.Start();
            if(listaImagenesMicroscopioZoom.Length < 3){
                Debug.LogWarning("La muestra: "  + this.gameObject.name + " no cuenta con los 3 sprites de acercamiento de zoom, por consiguiente se ha destruido");
                Destroy(this.gameObject);
            }
        }

        // Método de llamada de Unity, se llama en cada frame del PC
        // Se interpola el objeto adjunto a este script
        new private void Update() {
            base.Update();
        }

        // Método asbtracto que se hereda, para activar la funcionalidad de este objeto
        public override void ActivarFuncionalidad() {
            bool estadoColocarMuestra = microscopio.ColocarPortaobjetos(this);
            if(estadoColocarMuestra){
                base.ActivarFuncionalidad();
            }
        }

        // Método que permite resetear los parametros del mechero
        public override void ResetearFuncionalidad(){

        }

        public Sprite[] ListaImagenesMicroscopioZoom { set => listaImagenesMicroscopioZoom = value; get => listaImagenesMicroscopioZoom; }
    }
}