using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public abstract class ObjetoEstatico : Objeto {

        [SerializeField] private bool estadoObjeto;

        private Animator animObjeto;

        private void Awake(){
            animObjeto = GetComponent<Animator>();
        }

        new virtual protected void Start(){
            base.Start();
        }

        public abstract void ActivarFuncionalidad();

        /*
        public override void OnMouseEnter() {
        }

        public override void OnMouseExit() {
        }
        */
    }
}