using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(HingeJoint))]

    public class LectorAnguloBisagra : MonoBehaviour {

        [SerializeField] private bool invertirvalor = false;

        [Tooltip("Para objetos ligeramente descentrados." +
            "\nEl valor abs mínimo requerido para devolver un valor distinto de cero\n" +
            "- si playRange es 0.1, debe mover el dispositivo un 10% para obtener un resultado")]
        [SerializeField] private float rangoJuego = 0.05f;

        private HingeJoint articulacion;
        protected float valor = 0;
        private Quaternion rotacionInicial;
        private Quaternion rotacionPrincipalDelta;

        protected void Start(){
            articulacion = GetComponent<HingeJoint>();
            rotacionInicial = this.transform.localRotation;
            enabled = false;
        }

        public float ObtenerValorRotacion(){
            valor = articulacion.angle/(articulacion.limits.max - articulacion.limits.min) * 2;
            valor = invertirvalor ? -valor: valor;
            if(Mathf.Abs(valor) < rangoJuego) valor = 0;
            return Mathf.Clamp(valor, -1, 1);
        }
    }
}