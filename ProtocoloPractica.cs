using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(BoxCollider))]

    public class ProtocoloPractica : MonoBehaviour {
        
        [SerializeField] private Bata bata;
        [SerializeField] private Puerta[] puertasLab;
        [SerializeField] private bool usarConfiguracionBase = true;

        private BoxCollider cajaColision;

        private void Awake(){
            cajaColision = GetComponent<BoxCollider>();
            if(puertasLab.Length != 2){
                Debug.LogWarning("No se asigando los script de las puertas del Lab al protocolo de práctica");
            }
        }

        private void Start(){
            if(cajaColision){
                cajaColision.enabled = true;
                cajaColision.isTrigger = false;
            }
        }

        private void OnValidate() {
            if(usarConfiguracionBase){
                cajaColision = GetComponent<BoxCollider>();
                cajaColision.center = new Vector3(0, 1.1f, 0);
                cajaColision.size = new Vector3(0.15f, 2.2f, 1.9f);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")){
                Debug.Log("Protocolo");
                ValidarProtocoloPractica();
            }
        }

        private void ValidarProtocoloPractica(){
            bool estado = bata.EstadoAtuendo;
            cajaColision.enabled = !estado;
            if(!estado){
                if(puertasLab.Length != 2){
                    Debug.Log("Retun");
                    return;
                }
                puertasLab[0].ResetearFuncionalidad();
                puertasLab[1].ResetearFuncionalidad();
            }
        }
    }
}