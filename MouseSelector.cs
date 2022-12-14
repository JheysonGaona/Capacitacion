using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Capacitacion {

    public class MouseSelector : MonoBehaviour {
        
        // Variables de la clase
        [Header("Parámetros de configuración")]
        [Tooltip("Distancia en metros para poder interactuar con un objeto, se calcula desde el punto actual del personaje")]
        [SerializeField] private float distanciaInteraccion = 5.0f;

        [Tooltip("Capa 'Layer' sobre el cual el ray cast no tendra efecto sobre las coliciones")]
        [SerializeField] private LayerMask mascara = -1;
        
        private bool arrastrandoObjeto;
        private float alturaArrastrarObjeto;
        private Objeto objeto;
        private Vector3 compensar;
        private Vector3 posicionObjetivo;
        private Camera camaraPrincipal;
        private Transform seleccionarObjetivo;
        private CajaInformacion cajaInformacion;

        // => private bool cursorSobreUI;

        // Método de llamada de unity, se llama una vez por escena, se instancia la caja de información
        private void Awake(){
            if(TryGetComponent(out CajaInformacion cajaInformacion)) this.cajaInformacion = cajaInformacion;
        }

        // Método de llamada de Unity, se instancia la cámara principal de la escena
        private void Start() {
            camaraPrincipal = Camera.main;
            arrastrandoObjeto = false;
            if(camaraPrincipal == null) Debug.LogWarning ("Advertencia, no se ha encontrado " + 
                                        "una cámara principal dentro de la escena del juego");
        }

        // Método de llamada de Unity, se llama en cada Frame del computador 
        private void Update() {
            // Se valida si existe una cámara principal en el juego
            if(camaraPrincipal == null) return;
            // Se llama al método SeleccionarObjetoConClicMouse
            // => cursorSobreUI = EventSystem.current.IsPointerOverGameObject();
            // => if(!cursorSobreUI)
            ObtenerClicRaton();
            if(arrastrandoObjeto) MoverObjetoSeleccionado();
        }

        // Método de llamada de Unity, se actualiza en cada Fixed Update del computador, es Fijo 0,02 llamadas por frame
        private void FixedUpdate(){
            if(!arrastrandoObjeto /**&& !cursorSobreUI **/) SelectorDeObjetos();
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            camaraPrincipal = Camera.main;
            Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
            Gizmos.DrawRay(ray.origin, ray.direction * distanciaInteraccion);
        }
    #endif

        // Método que permite al puntero del ratón seleccionar solo Objetos de interacción
        private void SelectorDeObjetos(){
            RaycastHit hit;
            Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, distanciaInteraccion, mascara, QueryTriggerInteraction.Collide)) {
                if(hit.collider != null){
                    if(!hit.collider.CompareTag("Objeto")){
                        if(this.objeto != null && seleccionarObjetivo == null){
                            BorrarContenidoObjeto();
                        }
                        return;
                    }
                    if(this.objeto == null && hit.collider.TryGetComponent(out Objeto objeto)){
                        this.objeto = objeto;
                        if(cajaInformacion != null){
                            cajaInformacion.CargarInformacion(objeto.Nombre, objeto.Descripcion);
                        }
                    }
                }
            }else{
                if(this.objeto != null){
                    BorrarContenidoObjeto();
                }
            }
        }

        // Método que permite seleccionar o activar un objeto interactuable dentro de la escena
        private void ObtenerClicRaton(){
            // Se valida si el usuario presiona clic izquierdo
            if(Input.GetMouseButtonDown(0)){
                // Se efectua el evento del ray cast de la cámara
                RaycastHit hit;
                Vector3 posicionRaton = Input.mousePosition;
                Ray ray = camaraPrincipal.ScreenPointToRay(posicionRaton);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, distanciaInteraccion, mascara, QueryTriggerInteraction.Collide)) {
                    if(hit.collider != null){
                        if(!hit.collider.CompareTag("Objeto")){
                            return;
                        }

                        // Si es un objeto, se procede a activar su funcionalidad
                        if(hit.collider.TryGetComponent(out Objeto objeto)){
                            objeto.ActivarFuncionalidad();
                            if(objeto.ObtenerTipoObjeto()){
                                this.objeto = objeto;
                                Cursor.visible = false;
                                arrastrandoObjeto = true;
                                seleccionarObjetivo = hit.collider.transform;
                            }
                        }
                    }
                }
            }

            // Se valida si el usuario suelta el clic izquierdo del mouse
            if(Input.GetMouseButtonUp(0) && arrastrandoObjeto){
                Cursor.visible = true;
                arrastrandoObjeto = false;
                seleccionarObjetivo = null;
                objeto.ResetearFuncionalidad();
                BorrarContenidoObjeto();
            }

            if(Input.GetMouseButtonDown(1)){
                // Se efectua el evento del ray cast de la cámara
                RaycastHit hit;
                Vector3 posicionRaton = Input.mousePosition;
                Ray ray = camaraPrincipal.ScreenPointToRay(posicionRaton);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, distanciaInteraccion, mascara, QueryTriggerInteraction.Collide)) {
                    if(hit.collider != null){
                        if(!hit.collider.TryGetComponent(out IActivarObjeto iaObjeto)){
                            return;
                        }
                        iaObjeto.ActivarSegundaFuncionalidad();
                    }
                }
            }
        }

        // Método que permite mover el objeto que se ha selecionado con el Ratón
        private void MoverObjetoSeleccionado(){
            if(seleccionarObjetivo != null){
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camaraPrincipal.WorldToScreenPoint(seleccionarObjetivo.position).z);
                Vector3 worldPosition = camaraPrincipal.ScreenToWorldPoint(position);
                seleccionarObjetivo.position = new Vector3(worldPosition.x, 1.25f, worldPosition.z);
            }
        }

        private void BorrarContenidoObjeto(){
            this.objeto = null;
            if(cajaInformacion != null){
                cajaInformacion.BorrarInformacion();
            }
        }
    }
}