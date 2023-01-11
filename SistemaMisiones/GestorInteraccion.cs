using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Capacitacion {

    public class GestorInteraccion : MonoBehaviour {

        [SerializeField] private TMP_Text textoNombreNpc;
        [SerializeField] private TMP_Text textoDescripcionMision;
        [SerializeField] private Button btnAceptarMision;
        [SerializeField] private Button btnCancelarMision;

        private SistemaMision sistemaMision;

        private const string formatoInicio = "<color=white><b>";
        private const string formatoFin = "</b></color><br>";

        private void Start(){
            btnAceptarMision.onClick.AddListener( () => AceptarMision() );
            btnCancelarMision.onClick.AddListener( () => RechazarMision() );
        }

        private void CargarContenidoMision(Mision nuevaMision, string nameNpc){
            textoNombreNpc.text = nameNpc;
            textoDescripcionMision.text = formatoInicio + nuevaMision.TituloDeMision + formatoFin + nuevaMision.DetalleDeMision;
        }

        private void BorrarContenidoMision(){
            textoNombreNpc.text = "";
            textoDescripcionMision.text = "";
        }

        private void AceptarMision(){
            sistemaMision.Iniciar();
        }

        private void RechazarMision(){
            BorrarContenidoMision();
        }
    }
}