using UnityEngine;
using System.IO;

namespace SDF.Model
{
    public class SphereNode : SDFObjNode
    {
        public Vector3 p;
        public float s = 1;

        public override string Calcsd(int i)
        {
            Debug.Log("Write a sphere code.");
            string str = "float dist" + i + " = sdSphere(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), " + this.s + ");";
            return str;
        }
    }
}

