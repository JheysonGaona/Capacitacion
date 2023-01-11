using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [CreateAssetMenu(menuName = "Create Mission", fileName = "Mission", order = 0)]
    public class Mision : ScriptableObject {

        [Header("Detalles de la mision")]
        [SerializeField] private string tituloDeMision;
        [SerializeField] private string detalleDeMision;

        public enum TipoMision{
            RealizarPractica,
            ObtenerNotaMinima,
            ObtenerNotaMaxima,
        }

        [SerializeField] private TipoMision tipoDeMision = TipoMision.RealizarPractica;

        [Header("Mision en cadena")]
        [SerializeField] private bool tieneMisionEnCadena = false;
        [SerializeField] private Mision misionEnCadena;


        public string TituloDeMision => tituloDeMision;
        public string DetalleDeMision => detalleDeMision;
        public Mision MisionEnCadena => misionEnCadena;
    }
}