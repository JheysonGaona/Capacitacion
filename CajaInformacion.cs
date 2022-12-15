using UnityEngine;
using TMPro;

namespace Capacitacion {

    public class CajaInformacion : MonoBehaviour {

        // variables de la clase
        [Tooltip("Panel contenedor donde se mostrara la información, se utiliza para activarlo u ocultarlo")]
        [SerializeField] private GameObject panelContenedor;

        [Tooltip("Etiqueta de texto que se utiliza para establecer el nombre del objeto")]
        [SerializeField] private TMP_Text textoNombre;

        [Tooltip("Etiqueta de texto que se utiliza para establecer la descripción del objeto")]
        [SerializeField] private TMP_Text textoDescripcion;

        // Método de llamada de unity, se llama una única vez al iniciar el script
        private void Start() {
            panelContenedor.SetActive(false);
        }

        // Método que permite borrar la información del objeto
        public void BorrarInformacion(){
            textoNombre.text = "";
            textoDescripcion.text = "";
            panelContenedor.SetActive(false);
        }

        // Método que permite asignar la información del objeto
        public void CargarInformacion(string nombre, string descripcion){
            textoNombre.text = nombre;
            textoDescripcion.text = descripcion;
            panelContenedor.SetActive(true);
        }
    }
}