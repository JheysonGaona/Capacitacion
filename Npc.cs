using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion{

    public class Npc : ObjetoDinamico {

        private Animator anim;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes, por consiguiente hereda
        new protected virtual void Awake() {
            base.Awake();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
        }

        public override void ActivarFuncionalidad() {
            throw new System.NotImplementedException();
        }

        public override void EstablecerTipoObjeto() {
            throw new System.NotImplementedException();
        }

        public override void ResetearFuncionalidad() {
            throw new System.NotImplementedException();
        }
    }
}