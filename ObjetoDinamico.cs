using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(AudioSource))]

    public abstract class ObjetoDinamico : Objeto {

        // Variables de la clase
        
        [Tooltip("Clip de sonido que tendra el objeto al activar o desactivar su estado")]
        [SerializeField] private AudioClip clipSonidoObjeto;

        [Tooltip("Si se establece verdadero, el sonido se volvera a reproducir una vez que haya finalizado")]
        [SerializeField] protected bool activarSonidoBucle = false;

        protected AudioSource recursoAudio;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes
        protected virtual void Awake(){
            recursoAudio = GetComponent<AudioSource>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            ConfigurarRecursoSonido();
        }

        //Método que permite configurar el recurso de audio
        private void ConfigurarRecursoSonido(){
            recursoAudio.clip = clipSonidoObjeto;
            recursoAudio.playOnAwake = false;
            recursoAudio.loop = activarSonidoBucle;
        }

        // Métodoque permite activar el sonido del objeto
        protected void ActivarSonido(){
            recursoAudio.Play();
        }

        // Métodoque permite desactivar el sonido del objeto
        protected void DesactivarSonido(){
            recursoAudio.Stop();
        }

        // Método abstracto, de uso oblgatorio para demás clases que hereden de esta
        public abstract void ActivarFuncionalidad();
        public abstract void ResetearFuncionalidad();

    /*
        public override void OnMouseEnter(){
            material.material.color = colorObjetoSeleccionado;
        }

        public override void OnMouseExit(){
            material.material.color = colorOriginal;
        }
    */

    }
}
