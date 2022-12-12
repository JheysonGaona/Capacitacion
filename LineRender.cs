using UnityEngine;

namespace Capacitacion {

    public class LineRender : MonoBehaviour {

        // Variables de la clase
        [SerializeField] private Transform puntoConexion;
        [SerializeField] private float anchoLineaInicio = 0.015f;
        [SerializeField] private float anchoLineaDestino = 0.015f;
        [SerializeField] private Color colorInicio = new Color(255, 0, 0, 1);
        [SerializeField] private Color colorFinal = new Color(255, 255, 255, 0.5f);

        private LineRenderer lineRenderer;
        private Transform[] wayPoint;

        // Método de llamada de Unity, se instancian los componentes
        private void Awake(){
            GameObject newGameObject = new GameObject("LineRenderer");
            newGameObject.AddComponent<LineRenderer>();
            newGameObject.transform.SetParent(this.transform);
            lineRenderer = newGameObject.GetComponent<LineRenderer>();
        }

        // Método de llamada de Unity, se configura las lineas de renderizado y el objetivo
        private void Start(){
            wayPoint = new Transform[2];
            wayPoint[0] = this.transform;
            wayPoint[1] = puntoConexion;
            lineRenderer.startColor = colorInicio;
            lineRenderer.endColor = colorFinal;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startWidth = anchoLineaInicio;
            lineRenderer.endWidth = anchoLineaDestino;
            lineRenderer.positionCount = wayPoint.Length;
            DibujarLinea();
            enabled = false;
        }

        // Método de llamada de Unity, las lineas de renderizado se dibujan al finalizar cada frame
        private void DibujarLinea() {
            for(int i = 0; i < wayPoint.Length; i++){
                lineRenderer.SetPosition(i, wayPoint[i].position);
            }
        }

        // Método de Unity, permite activar la funcionalidad de script cuando la cámara renderiza el objeto
        private void OnBecameVisible() {
            enabled = true;
        }

        // Método de Unity, permite descactivar la funcionalidad de script cuando la cámara no renderiza el objeto
        private void OnBecameInvisible() {
            enabled = false;
        }
    }
}