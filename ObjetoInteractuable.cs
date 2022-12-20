using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(Rigidbody))]

    public class ObjetoInteractuable : Objeto, IActivarObjeto {

        [SerializeField] private bool puedeRotar = false;
        [SerializeField] private bool activarFisicas = false;
        [SerializeField] [Range(0.25f, 4.0f)] private float alturaAlzarceObjeto = 1.25f;
        [SerializeField] private Vector3 anguloRotación = new Vector3(0, 0, 0);

        private Rigidbody componenteFisica;
        private Vector3 anguloRotacionInicial;
        private bool elObjetoRoto = false;
        public bool activarFuncionalidad { get => elObjetoRoto; set => elObjetoRoto = value; }

        // Método de llamada de Unity, se ejecuta una sola vez al iniciar el aplicativo
        // Se instancian los componentes
        protected virtual void Awake(){
            componenteFisica = GetComponent<Rigidbody>();
        }

        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se hereda el método Start de la clase Objeto
        new protected virtual void Start() {
            base.Start();
            componenteFisica.useGravity = true;
            componenteFisica.isKinematic = false;
            EstablecerRecursoFisicaRealismo();
            anguloRotacionInicial = this.transform.rotation.eulerAngles;
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

        public override void ActivarFuncionalidad() {
            this.transform.rotation = Quaternion.Euler(anguloRotacionInicial);
            componenteFisica.useGravity = false;
            CongelarRotacionObjeto();
        }

        public void ActivarSegundaFuncionalidad() {
            if(puedeRotar){
                elObjetoRoto = !elObjetoRoto;
                this.transform.rotation = elObjetoRoto ? Quaternion.Euler(anguloRotación): Quaternion.Euler(anguloRotacionInicial);
            }
        }

        public override void ResetearFuncionalidad(){
            elObjetoRoto = false;
            componenteFisica.useGravity = true;
            componenteFisica.velocity = new Vector3(0, 0, 0);
            EstablecerRecursoFisicaRealismo();
        }

        public override void EstablecerTipoObjeto() {
            this.caracteristicaObjeto = tipoObjeto.Movible;
        }

        public float AlturaAlzarceObjeto { set => alturaAlzarceObjeto = value; get => alturaAlzarceObjeto; }
    }
}