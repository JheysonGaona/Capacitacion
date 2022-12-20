using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(BoxCollider))]

    public class ZonaMuestra : MonoBehaviour {

        [SerializeField] private Microscopio microscopio;

        private BoxCollider cajaColision;

        private void Awake() {
            cajaColision = GetComponent<BoxCollider>();
        }

        private void Start() {
            cajaColision.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.TryGetComponent(out Objetivo objetivoFijado)){
                microscopio.CambiarLenteObjetivo(objetivoFijado.IdObjetivo, objetivoFijado.ResolucionObjetivo);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.TryGetComponent(out Objetivo objetivoFijado)){
                microscopio.CambiarLenteObjetivo(-1, "");
            }
        }
    }
}