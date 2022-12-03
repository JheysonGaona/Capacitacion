using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public abstract class ObjetoEstatico : Objeto {



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