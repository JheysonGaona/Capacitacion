using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    public class Bata : ObjetoDinamico {

        [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererPersonaje;
        [SerializeField] private Mesh atuendoPersonaje;
        [SerializeField] private Mesh atuendoBata;

        [SerializeField] private GameObject manosPersonaje;

        [SerializeField] private Material materialAtuendoPersonaje;
        [SerializeField] private Material materialAtuendoBata;

        private bool estadoAtuendo = false;
        private MeshFilter meshBata;
        private Renderer renderBata;

        new protected virtual void Awake(){
            base.Awake();
            meshBata = GetComponent<MeshFilter>();
            renderBata = GetComponent<Renderer>();
        }

        new protected virtual void Start(){
            base.Start();
            meshBata.mesh = atuendoBata;
            renderBata.material = materialAtuendoBata;
        }

        public override void ActivarFuncionalidad() {
            estadoAtuendo = !estadoAtuendo;
            base.ActivarFuncionalidad();
            if(estadoAtuendo){
                skinnedMeshRendererPersonaje.sharedMesh = atuendoBata;
                skinnedMeshRendererPersonaje.gameObject.GetComponent<Renderer>().material = materialAtuendoBata;
                meshBata.mesh = atuendoPersonaje;
                renderBata.material = materialAtuendoPersonaje;
            }else{
                skinnedMeshRendererPersonaje.gameObject.GetComponent<Renderer>().material = materialAtuendoPersonaje;
                skinnedMeshRendererPersonaje.sharedMesh = atuendoPersonaje;
                meshBata.mesh = atuendoBata;
            renderBata.material = materialAtuendoBata;

            }
            if(manosPersonaje != null) manosPersonaje.SetActive(!estadoAtuendo);
        }

        public bool EstadoAtuendo { get => estadoAtuendo; }
    }
}