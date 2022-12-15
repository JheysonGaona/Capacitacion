using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(Rigidbody))]

    public class ObjetoInteractuable : Objeto {

        [SerializeField] [Range(0.5f, 4.0f)] private float alturaAlzarceObjeto = 1.25f;
        [SerializeField] private Vector3 anguloRotación;
        [SerializeField] private bool activarFisicas = false;
        [SerializeField] private bool puedeRotar = false;

        private Rigidbody componenteFisica;
        private Vector3 anguloRotacionInicial;
        private bool elObjetoRoto = false;

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes
        protected virtual void Awake(){
            componenteFisica = GetComponent<Rigidbody>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            ConfigurarRecursoFisica();
            anguloRotacionInicial = this.transform.rotation.eulerAngles;
        }

        // Método que permite configurar el componente de fisicas
        private void ConfigurarRecursoFisica(){
            componenteFisica.useGravity = true;
            componenteFisica.isKinematic = false;
            EstablecerRecursoFisicaRealismo();
        }

        public Objeto SeleccionarObjeto(){
            componenteFisica.useGravity = false;
            this.transform.rotation = Quaternion.Euler(anguloRotacionInicial);
            CongelarRotacionObjeto();
            return this;
        }

        public override void ActivarFuncionalidad() {
            if(puedeRotar){
                elObjetoRoto = !elObjetoRoto;
                if(elObjetoRoto){
                    this.transform.rotation = Quaternion.Euler(anguloRotación);
                }else{
                    this.transform.rotation = Quaternion.Euler(anguloRotacionInicial);
                }
            }
        }

        public override void ResetearFuncionalidad(){
            elObjetoRoto = false;
            componenteFisica.useGravity = true;
            componenteFisica.velocity = new Vector3(0, 0, 0);
            // this.transform.rotation = Quaternion.Euler(anguloRotacionInicial);
            EstablecerRecursoFisicaRealismo();
        }

        private void EstablecerRecursoFisicaRealismo(){
            if( activarFisicas ){
                componenteFisica.constraints =  RigidbodyConstraints.None;
            }else{
                CongelarRotacionObjeto();
            }
        }

        private void CongelarRotacionObjeto(){
            componenteFisica.constraints =  RigidbodyConstraints.FreezeRotationX |
                                            RigidbodyConstraints.FreezeRotationY |
                                            RigidbodyConstraints.FreezeRotationZ;
        }

        public float AlturaAlzarceObjeto { set => alturaAlzarceObjeto = value; get => alturaAlzarceObjeto; }
    }
}