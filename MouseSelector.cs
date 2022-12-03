using UnityEngine;

namespace Capacitacion {

    public class MouseSelector : MonoBehaviour {

        [Header("Parámetros de la UI")]
        [Tooltip("Se determina la altura de la caja de información cuando se selecciona un objeto")]
        [SerializeField] private float alturaCajaInformacion;

        [Tooltip("Se determina la anchura de la caja de información cuando se selecciona un objeto")]
        [SerializeField] private float anchuraCajaInformacion;
        
        [Header("Parámetros de configuración")]
        [Tooltip("Distancia en metros para poder interactuar con un objeto, se calcula desde el punto actual del personaje")]
        [SerializeField] private float distanciaInteraccion = 5.0f;

        [Tooltip("Capa 'Layer' sobre el cual el ray cast no tendra efecto sobre las coliciones")]
        [SerializeField] private LayerMask mascara = -1;

        public Objeto objeto;
        private Vector3 compensar;
        private Vector3 posicionObjetivo;
        public Transform seleccionarObjetivo;
        private Camera camaraPrincipal;
        private bool arrastrandoObjeto;

        private void OnGUI() {
            if(objeto != null){
                GUI.Box(new Rect(Input.mousePosition.x, Input.mousePosition.y, anchuraCajaInformacion, alturaCajaInformacion), "Hola" );
            }
        }

        // Método de llamada de Unity, se instancia la cámara principal de la escena
        private void Start() {
            camaraPrincipal = Camera.main;
            arrastrandoObjeto = false;
            if(camaraPrincipal == null) Debug.LogWarning ("Advertencia, no se ha encontrado" + 
                                        "una cámara principal dentro de la escena del juego");
        }

        // Método de llamada de Unity, se llama en cada Frame del computador 
        private void Update() {
            // Se valida si existe una cámara principal en el juego
            if(camaraPrincipal == null) return;
            // Se llama al método SeleccionarObjetoConClicMouse
            ObtenerClicRaton();
        }

        private void LateUpdate(){
            // Se valida si se tiene un objeto seleccionado
            if(arrastrandoObjeto) MoverObjetoSeleccionado();
        }

        // Método de llamada de Unity, se actualiza en cada Fixed Update del computador, es Fijo 0,02 llamadas por frame
        private void FixedUpdate(){
            SelectorDeObjetos();
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
                if(objeto == null){
                    if(hit.collider.TryGetComponent(out Objeto objeto)){
                        this.objeto = objeto;
                    }
                }
            }else{
                if(objeto != null){
                    this.objeto = null;
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

                        // Si es un objeto dinámico, se procede a activar su funcionalidad
                        if(hit.collider.gameObject.TryGetComponent(out ObjetoDinamico objetoDinamico)){
                            objetoDinamico.ActivarFuncionalidad();
                            return;
                        }

                        if(hit.collider.TryGetComponent(out ObjetoInteractuable objetoInteractuable)){
                            Vector3 screenMousePosFar = new Vector3(posicionRaton.x, posicionRaton.y, camaraPrincipal.farClipPlane);
                            Vector3 screenMousePosNear = new Vector3(posicionRaton.x, posicionRaton.y, camaraPrincipal.nearClipPlane);
                            Vector3 worldMousePosFar = camaraPrincipal.ScreenToWorldPoint(screenMousePosFar);
                            Vector3 worldMousePosNear = camaraPrincipal.ScreenToWorldPoint(screenMousePosNear);

                            // RaycastHit hit;
                            // Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
                            seleccionarObjetivo = hit.collider.transform;
                            arrastrandoObjeto = true;
                            Cursor.visible = false;
                            // Si el objeto cuenta con propiedades de Físicas, se desabilita la gravedad
                            if(seleccionarObjetivo.TryGetComponent(out Rigidbody rigidbody)){
                                rigidbody.useGravity = false;
                            }
                        // Si no es ningún objeto, se establece a null el objetivo a seleccionar
                        }else{
                            seleccionarObjetivo = null;
                        }
                    }
                }
            }

            // Se valida si el usuario suelta el clic izquierdo del mouse
            if(Input.GetMouseButtonUp(0) && arrastrandoObjeto){
                arrastrandoObjeto = false;
                Cursor.visible = true;
                if(seleccionarObjetivo.TryGetComponent(out Rigidbody rigidbody)){
                    rigidbody.useGravity = true;
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
    }
}