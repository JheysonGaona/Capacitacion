using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capacitacion {

    [RequireComponent(typeof(HingeJoint))]

    public class RotarConLimite : ObjetoRotatorio {
        
        [SerializeField] [Range(0, 180)] private int limiteMaxRotacion = 90;
        [SerializeField] [Range(-180, 0)] private int limiteMinRotacion = -90;

        public enum AxisRotacion{ Axis_X, Axis_Y, Axis_Z }
        public AxisRotacion ejeRotacion = AxisRotacion.Axis_Z;

        private HingeJoint articulacion;

        private void Awake(){
            articulacion = GetComponent<HingeJoint>();
        }

        new protected void Start(){
            base.Start();
            JointLimits limites = articulacion.limits;
            limites.min = limiteMinRotacion;
            limites.max = limiteMaxRotacion;
            articulacion.limits = limites;
            articulacion.useLimits = true;
            EstablecerLadoRotacion();
        }

        private void EstablecerLadoRotacion(){
            if(ejeRotacion == AxisRotacion.Axis_X){
                articulacion.axis = new Vector3(1, 0, 0);
            }else if(ejeRotacion == AxisRotacion.Axis_Y){
                articulacion.axis = new Vector3(0, 1, 0);
            }else{
                articulacion.axis = new Vector3(0, 0, 1);
            }
        }
    }
}