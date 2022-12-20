using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(AudioSource))]

    public abstract class ObjetoDinamico : Objeto {

        // Variables de la clase
        [Tooltip("Clip de sonido que tendra el objeto al activar o desactivar su estado")]
        [SerializeField] private AudioClip clipSonidoObjeto;

        [Tooltip("Si se establece verdadero, el sonido se volvera a reproducir una vez que haya finalizado")]
        [SerializeField] private bool activarSonidoBucle = false;

        private AudioSource recursoAudio;

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

        // Método abstracto, de uso oblgatorio para demás clases que hereden de esta
        public override void ActivarFuncionalidad(){
            recursoAudio.Play();
        }

        public override void ResetearFuncionalidad(){
            recursoAudio.Stop();
        }

        public override void EstablecerTipoObjeto() {
            this.caracteristicaObjeto = tipoObjeto.Estatico;
        }
    }
}
