using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(AudioSource))]

    public class MatrazAtmosfera : MonoBehaviour {
        
        // Variables del script
        private MatrazOceano matrazOceano;
        private ParticleSystem[] efectoGases;
        private ParticleSystem efectoElectricidad;
        private ParticleSystem efectoGotasAgua;
        private bool activarTemporizador;
        private bool activarReaccion;
        private bool[] activarGas;
        private bool activarElectricidad;
        private float tiempoReaccion;
        private float tiempoReaccionOriginal;
        private AudioSource recursoSonido;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se carga el recurso de sonido
        private void Awake() {
            recursoSonido = GetComponent<AudioSource>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo, se instancia la variable
        private void Start() {
            activarGas = new bool[efectoGases.Length];
            tiempoReaccionOriginal = tiempoReaccion;
            recursoSonido.loop = true;
            recursoSonido.playOnAwake = false;
        }

        // Método que permite activar los gases dentro de la matraz
        public void InterruptorGas(int id){
            activarGas[id] = !activarGas[id];
            Interruptor(efectoGases[id], activarGas[id]);
        }

        // Método que permite activar el circuito eléctrico dentro de la matraz, además, activa el sonido de electricidad
        public void InterruptorElectricidad(){
            activarElectricidad = !activarElectricidad;            
            if(activarElectricidad){
                recursoSonido.Play();
            }else{
                recursoSonido.Stop();
            }
            Interruptor(efectoElectricidad, activarElectricidad);
        }

        // Método que permite activar el efecto de particulas en función al interruptor activado
        private void Interruptor(ParticleSystem efectoParticula, bool estado){
            if(estado){
                efectoParticula.Play();
            }else{
                efectoParticula.Stop();
            }
            ValidarReaccion();
        }

        // Método que permite verificar si la reacción entre vapor, gases y electricidad se encuentra habilitada
        private void ValidarReaccion(){
            bool combinacionGases = true;
            foreach(bool estadoActual in activarGas){
                if(!estadoActual){
                    combinacionGases = false;
                    break;
                }
            }
            bool estadoReaccion = matrazOceano.ValidarReaccion(combinacionGases, activarElectricidad);
            if(estadoReaccion){
                // Caen gotas de agua e inicia el temporizador
                if(activarReaccion){
                    efectoGotasAgua.Play();
                    matrazOceano.ColorAgua(activarReaccion);
                }else{
                    activarTemporizador = true;
                    InvokeRepeating(nameof(TemporizadorReaccionQuimica), 1, 1);
                }
            }else{
                // se detienen las gotas de agua e inicia el temporizador
                if(!activarReaccion && activarTemporizador){
                    activarTemporizador = false;
                    CancelInvoke("TemporizadorReaccionQuimica");
                    tiempoReaccion = tiempoReaccionOriginal;
                }else{
                    efectoGotasAgua.Stop();
                    matrazOceano.ColorAgua(activarReaccion);
                }
            }
        }

        // Método que permite iniciar un temporizador para la reacción quimica
        private void TemporizadorReaccionQuimica() {
            tiempoReaccion--;
            if (tiempoReaccion <= 0) {
                CancelInvoke("TemporizadorReaccionQuimica");
                efectoGotasAgua.Play();
                activarReaccion = true;
                matrazOceano.ColorAgua(activarReaccion);
            }
        }

        // Método que permite resetear los parametros de la práctica
        public void ResetearParametros(){
            tiempoReaccion = tiempoReaccionOriginal;
            if(activarReaccion){
                efectoGotasAgua.Stop();
                activarReaccion = false;
            }
        }

        // Getters & Setters de la clase
        public float TiempoReaccion { set => tiempoReaccion = value; get => tiempoReaccion; }
        public ParticleSystem[] EfectoGases { set => efectoGases = value; }
        public ParticleSystem EfectoElectricidad { set => efectoElectricidad = value; }
        public ParticleSystem EfectoGotasAgua { set => efectoGotasAgua = value; }
        public AudioSource RecursoSonido { get => recursoSonido; }
        public MatrazOceano MatrazOceano { set => matrazOceano = value; }
    }
}