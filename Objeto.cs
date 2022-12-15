using UnityEngine;

namespace Capacitacion {

    public abstract class Objeto : MonoBehaviour {
        
        // Variables de la clase
        [Header("Parámetros del objeto")]
        [Tooltip("Número de identificación del objeto, puede dejarlo en cero si no hará uso de este parámetro")]
        [SerializeField] private int identificador;

        [Tooltip("Nombre del objeto, es necesario asignar el nombre real")]
        [SerializeField] private string nombre;

        [Tooltip("Descripcción del objeto, se detalla la información relevante o para que sirve el objeto")]
        [SerializeField] private string descripcion;
        
        // Método de llamada de Unity, se ejecuta al inicial el aplicativo
        // Se asigna el tag y los layers del objeto, por si el usuario se olvida manualmente
        protected virtual void Start(){
            this.gameObject.tag = "Objeto";
            this.gameObject.layer = 8;
        }

        // Getters & Setters de la clase
        public int Identificador { set => identificador = value; get => identificador; }
        public string Nombre { set => nombre = value; get => nombre; }
        public string Descripcion { set => descripcion = value; get => descripcion; }

        // Método abstracto, de uso oblgatorio para demás clases que hereden de esta
        public abstract void ActivarFuncionalidad();
        public abstract void ResetearFuncionalidad();
    }
}