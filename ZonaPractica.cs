using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(SphereCollider))]

    public class ZonaPractica : MonoBehaviour {

        [SerializeField] private GameObject camaraZonaInteraccion;
        [SerializeField] private GameObject canvasPractica1;

        private Animator animatorJugador;
        private GameObject camaraPersonaje;
        private SphereCollider zonaColision;

        private void Awake(){
            zonaColision = GetComponent<SphereCollider>();
        }

        private void Start(){
            zonaColision.isTrigger = true;
            canvasPractica1.SetActive(false);
            camaraZonaInteraccion.SetActive(false);
            camaraPersonaje = GameObject.Find("CM Personaje");
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag.Equals("Player")){
                if(other.TryGetComponent(out Animator animatorJugador)){
                    CambiarCamara(true);
                    this.animatorJugador = animatorJugador;
                    this.animatorJugador.SetBool("canMove", false);
                }
            }
        }

        private void CambiarCamara(bool condicion){
            canvasPractica1.SetActive(condicion);
            camaraPersonaje.SetActive(!condicion);
            camaraZonaInteraccion.SetActive(condicion);
        }

        public void SalirDeLaPractica(){
            CambiarCamara(false);
            this.animatorJugador.SetBool("canMove", true);
        }
    }
}