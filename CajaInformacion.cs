using UnityEngine;
using TMPro;

namespace Capacitacion {

    public class CajaInformacion : MonoBehaviour {

        [SerializeField] private GameObject panelContenedor;
        [SerializeField] private TMP_Text textoNombre;
        [SerializeField] private TMP_Text textoDescripcion;

        // Start is called before the first frame update
        private void Start() {
            panelContenedor.SetActive(false);
        }

        public void BorrarInformacion(){
            textoNombre.text = "";
            textoDescripcion.text = "";
            panelContenedor.SetActive(false);
        }

        public void CargarInformacion(string nombre, string descripcion){
            textoNombre.text = nombre;
            textoDescripcion.text = descripcion;
            panelContenedor.SetActive(true);
        }
    }
}