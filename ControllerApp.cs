using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Capacitacion{

    public class ControllerApp : MonoBehaviour {

        public Button btn;

        public void Start(){
            btn.onClick.AddListener( EncenderOpcion2 );
        }
        
        public void EncenderOpcion1(){
            Debug.Log("Se ha encendido el fuego opcion 1");
        }

        private void EncenderOpcion2(){
            Debug.Log("Se ha encendido el fuego opcion 2");
        }
    }
}