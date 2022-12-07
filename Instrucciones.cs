using UnityEngine;
using UnityEngine.UI;

namespace Capacitacion {

    public class Instrucciones : MonoBehaviour {
        
        // Variables de la clase
        [SerializeField] private GameObject panelInstrucciones;
        [SerializeField] private Image imgInstrucciones;
        [SerializeField] private Button btnAnterior;
        [SerializeField] private Button btnSiguiente;
        [SerializeField] private Button btnCerrarInstruccion;

        [SerializeField] private Sprite[] imagenesIntruccion;

        private int limite = 0;
        private int contador = 0;
        public bool estadoPanelInstrucciones = false;

        // Método de llamada de Unity, se instancian los botones
        private void Start(){
            MostrarInstrucciones();
            limite = imagenesIntruccion.Length - 1;
            btnAnterior.onClick.AddListener( () => Anteriorinstruccion() );
            btnSiguiente.onClick.AddListener( () => Siguienteinstruccion() );
            btnCerrarInstruccion.onClick.AddListener( () => MostrarInstrucciones() );
            EstablecerContenidoInstruccion();
        }

        // Método que permite regresar a la instruccion anterior
        private void Anteriorinstruccion(){
            contador--;
            EstablecerContenidoInstruccion();
        }

        // Método que permite pasar a la siguiente instruccion
        private void Siguienteinstruccion(){
            contador++;
            EstablecerContenidoInstruccion();
        }

        // Método que valida el estado de los botones y carga la instruccion
        private void EstablecerContenidoInstruccion(){
            btnAnterior.interactable = contador <= 0 ? false: true;
            btnSiguiente.interactable = contador < limite ? true: false;

            imgInstrucciones.sprite = imagenesIntruccion[contador];
        }

        public void MostrarInstrucciones(){
            estadoPanelInstrucciones = !estadoPanelInstrucciones;
            panelInstrucciones.SetActive(estadoPanelInstrucciones);
            contador = 0;
            EstablecerContenidoInstruccion();
        }
    }
}