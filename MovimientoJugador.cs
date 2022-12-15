using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]

    public class MovimientoJugador : MonoBehaviour {
        
        // variables de la clase
        [Tooltip("Establece la velocidad de movimiento que tendra el personaje dentro del ambiente 3D")]
        [SerializeField] private float velocidadMovimiento = 2;

        [Tooltip("Se define la fuerza que se ejerce sobre el movimiento del personaje, a mayor escala mayor movilidad")]
        [SerializeField] [Range(0, 4)] private float multiplicadorVelocidadMovimiento = 1;

        [Tooltip("La velocidad de rotación del personaje, mientras mayor sea, más rapido rotará")]
        [SerializeField] private float velocidadRotacion = 180;

        [Tooltip("La fuerza de salto del personaje, a mayor escala mayor sera su será su altitud de salto")]
        [SerializeField] private float fuerzaSalto = 7;

        [Tooltip("Gravedad del personaje sobre el entorno 3D, no modificar este valor")]
        [SerializeField] private float gravedad = -9.8f;

        [Tooltip("Multiplicador de gravedad a menor escala, menor será la fuerza gravitatoria que ejerce sobre el personaje")]
        [SerializeField] [Range(0, 4)] private float multiplicadorGravedad = 1;

        [Tooltip("Si la casilla se encuentra desmarcada, el personaje no puedrá moverse o efectuar acción alguna")]
        [SerializeField] private bool puedeMoverse = false;

        [Tooltip("Si la casilla se encuentra desmarcada, el personaje no esta tocando el piso, por consiguiente se encuentra en el aire")]
        [SerializeField] private bool estaEnElSuelo = false;

        [Tooltip("Si la casilla se encuentra desmarcada, el personaje puede efectuar un salto")]
        [SerializeField] private bool saltar = false;

        [Tooltip("Se establece un tamañao para el ray cast, se mide desde el jugador hasta el suelo")]
        [SerializeField] private float comprobarDistanciaSuelo = 0.35f;

        [Tooltip("Capa 'Layer' sobre el cual el ray cast no tendra efecto sobre las coliciones")]
        [SerializeField] private LayerMask mascara = -1;

        private Animator anim;
        private Vector3 direccionMovimiento;
        private CharacterController controladorPersonaje;
        private float comprobarDistanciaSueloValorInicial;
        private float velocidadEntradaTeclado;
        private float velocidadCaida;
        private float horizontal;
        private float vertical;

        // Método de llamada de Unity, se establecen los componentes del personaje
        private void Awake(){
            anim = GetComponent<Animator>();
            controladorPersonaje = GetComponent<CharacterController>();
        }

        // Método de llama de Unity, se instancia el valor de la variable
        private void Start(){
            anim.SetBool("canMove", true);
            comprobarDistanciaSueloValorInicial = comprobarDistanciaSuelo;
        }

        // Método de llamada de Unity, se llama en cada frame del computador
        // Se establece la lógica de movimiento del personaje
        private void Update(){
            puedeMoverse = anim.GetBool("canMove");
            ValidarSuelo();
            if(puedeMoverse){
                // Lógica de movimiento del pj
                CapturarEntradaTeclado();
                if(estaEnElSuelo){
                    ControlarMovimientoSuelo();
                }else{
                    ControlarMovimientoAire();
                }
            }else{
                // Lógica para que el pj no se pueda mover
                direccionMovimiento = Vector3.zero;
                vertical = direccionMovimiento.magnitude;
                horizontal = direccionMovimiento.magnitude;
            }

            CalcularMovimiento();

            ControlarMovimiento();

            ControlarAnimaciones();
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos() {
            // m_CharacterController = GetComponent<CharacterController>();
            Gizmos.color = Color.red;
            // Gizmos.DrawWireSphere(transform.position  - (Vector3.up * 0.25f), m_CharacterController.height/m_RadiusSphere);
            Gizmos.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * comprobarDistanciaSuelo));
        }
    #endif

        // Método que permite validar si el personaje se encuentra tocando el piso
        private void ValidarSuelo(){
            RaycastHit hit;
            bool raycast = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, comprobarDistanciaSuelo, mascara, QueryTriggerInteraction.Ignore);
            if(raycast || controladorPersonaje.isGrounded){
                estaEnElSuelo = true;
            }else{
                estaEnElSuelo = false;
            }
        }

        // Método que permite capturar las entradas de teclado, es decir, si el usuario preesiona cierta tecla
        private void CapturarEntradaTeclado(){
            vertical = Input.GetAxis("Vertical");       // Entrada de teclas W, S y flechas arriba y abajo
            horizontal = Input.GetAxis("Horizontal");   // Entrada de teclas A, D y flechas derecha e izquierda
            saltar = Input.GetButtonDown("Jump");       // Entrada de tecla space
        }

        // Método que permite realizar acciones mientras el personaje este en el piso
        private void ControlarMovimientoSuelo(){
            if(saltar){
                velocidadCaida = fuerzaSalto;
                comprobarDistanciaSuelo = 0.01f;
            }else{
                velocidadCaida = -2;
            }
        }

        // Método que permite realizar acciones mientras el personaje este en el aire
        private void ControlarMovimientoAire(){
            velocidadCaida += (gravedad * multiplicadorGravedad) * Time.deltaTime;
            comprobarDistanciaSuelo = controladorPersonaje.velocity.y < 0 ? comprobarDistanciaSueloValorInicial: 0.01f;
        }

        // Método que permite calcular el movimiento del personaje en función a su velocidad y hacia donde se este moviendo
        private void CalcularMovimiento(){
            // direccionMovimiento = transform.forward * vertical + transform.right * horizontal;
            direccionMovimiento = transform.forward * vertical;
            transform.Rotate(0, velocidadRotacion * horizontal * Time.deltaTime, 0);
            velocidadEntradaTeclado = Mathf.Clamp01(direccionMovimiento.magnitude);
            direccionMovimiento = direccionMovimiento.normalized;
            // direccionMovimiento *= velocidadMovimiento * velocidadEntradaTeclado * multiplicadorVelocidadMovimiento;
            direccionMovimiento = vertical < 0 ? direccionMovimiento * multiplicadorVelocidadMovimiento * velocidadMovimiento: (direccionMovimiento * multiplicadorVelocidadMovimiento * velocidadMovimiento)/0.75f;
            direccionMovimiento *= velocidadEntradaTeclado;
            direccionMovimiento.y = velocidadCaida;
        }

        // Método que permite controlar el movimiento del personaje, mueve el character controller
        private void ControlarMovimiento(){  
            controladorPersonaje.Move(direccionMovimiento * Time.deltaTime);
        }

        // Método que se utiliza para activar las animaciones del personaje, en función a sus acciones
        private void ControlarAnimaciones(){
            anim.SetFloat("vertical", vertical);
            anim.SetFloat("horizontal", horizontal);
            anim.SetBool("onGround", estaEnElSuelo);
            if(saltar && estaEnElSuelo){
                anim.SetTrigger("jump");
            }
        }

        private void MoverseAlObjetivo(Transform objetivo){
            
        }
    }
}