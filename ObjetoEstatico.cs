using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public abstract class ObjetoEstatico : Objeto {

        protected virtual void Awake(){

        }

        new protected virtual void Start(){
            base.Start();
        }
    }
}