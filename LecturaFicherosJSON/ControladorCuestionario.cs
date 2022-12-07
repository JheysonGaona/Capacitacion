using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Capacitacion {

    public class ControladorCuestionario : MonoBehaviour {

        // Variables de la clase
        [SerializeField] private string nombreFichero;
        public GameObject PnlContenedorCuestionario;
        public Transform PnlPadrePreguntas;
        public Scrollbar ScrollVertical;
        public TMP_Text TextoCargarPregunta;
        public GameObject BtnPrefabPregunta;

        private Pregunta[] todasPreguntasCuestionario;
        private int idPregunta = 0;

        // Método de llamada de Unity, se inicializa la lectura del json y se prepara el cuestionario
        private void Start(){
            PnlContenedorCuestionario.SetActive(false);
            if(!string.IsNullOrEmpty(nombreFichero)){
                // Se lee el archivo el datos desde la carpeta Resources y se lo establece como arreglo de clase Pregunta
                TextAsset jsonTextFile = Resources.Load<TextAsset>(nombreFichero);
                todasPreguntasCuestionario = JsonHelper.FromJson<Pregunta>(jsonTextFile.text);
            }else{
                Debug.LogWarning("No se ha ingresado un nombreválido para el archivo de datos del cuestionario");
            }
        }

        // Método que permite cargar la pregunta del cuestionario en orden ascendente
        public void CargarPreguntaCuestionario(){
            PnlContenedorCuestionario.SetActive(true);
            if(idPregunta < todasPreguntasCuestionario.Length){
                Pregunta pregunta = todasPreguntasCuestionario[idPregunta];
                TextoCargarPregunta.text = pregunta.TextoPregunta;
                CargarBotonRespuesta(pregunta.Respuestas);
                idPregunta++;
            }else{
                // Se acabo el cuestionario
                PnlContenedorCuestionario.SetActive(false);
            }
        }

        // Método que instancia la lógica de las respuestas
        private void CargarBotonRespuesta(Respuesta[] respueta){
            PnlContenedorCuestionario.SetActive(true);
            DestruirBotonesDelContenedor();
            // Se cargan todas las respuestas de la pregunta que se carga
            foreach(Respuesta respuestaActual in respueta){
                Button botonPrefabricado = InstanciarBotones(respuestaActual.TextoRespuesta);
                AgregarEventoEscucha(botonPrefabricado, respuestaActual.ValorRespuesta);
            }
            ScrollVertical.value = 1;
        }

        // Método que destruye los componentes hijos de AnswerParent
        private void DestruirBotonesDelContenedor(){
            foreach(Transform hijo in PnlPadrePreguntas.transform){
                Destroy(hijo.gameObject);
            }
        }

        // Método que instancia la lógica de los botones de la UI, para las preguntas
        private Button InstanciarBotones(string text){
            GameObject copiaBotonPregunta = Instantiate(BtnPrefabPregunta, PnlPadrePreguntas, true);
            copiaBotonPregunta.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
            copiaBotonPregunta.transform.localScale = new Vector3(1, 1, 1);
            return copiaBotonPregunta.GetComponent<Button>();
        }

        // Método que permite agregar evento de click a los botones de preguntas
        private void AgregarEventoEscucha(Button button, bool valorRespuesta){
            button.onClick.AddListener( () => ValidarRespuesta(valorRespuesta) );
        }

        // Método que permite validar la respuesta en función a la pregunta
        private void ValidarRespuesta(bool valorRespuesta){
            if(valorRespuesta){
                Debug.Log("La respuesta es correcta");
            }else{
                Debug.Log("La respuesta es incorrecta");
            }
            CargarPreguntaCuestionario();
        }
    }
}