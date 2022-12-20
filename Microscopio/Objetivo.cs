using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Capacitacion {

    [RequireComponent(typeof(BoxCollider))]

    public class Objetivo : MonoBehaviour {

        [SerializeField] private int idObjetivo;
        [SerializeField] private string resolucionObjetivo = "X--";

        private BoxCollider cajaColision;

        private void Awake(){
            cajaColision = GetComponent<BoxCollider>();
        }

        private void Start(){
            cajaColision.isTrigger = false;
        }

        public int IdObjetivo { get => idObjetivo; }
        public string ResolucionObjetivo { get => resolucionObjetivo; }
    }
}