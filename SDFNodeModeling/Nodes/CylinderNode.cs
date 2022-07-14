using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Model
{
    public class CylinderNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public float h = 1;
        [Input] public float r = 1;
        public override string Calcsd(int i)
        {
            Debug.Log("Write a CylinderNode");
            string str;
            str = "float dist" + i + " = sdCylinder(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), " + this.h + ", " + this.r + ");";
            return str;
        }
    }
}
