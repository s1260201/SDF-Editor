using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Model
{
    public class TriangularPrismNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public Vector2 h;
        public override string calcsd(int i)
        {
            Debug.Log("Write a TriangularPrismNode");
            string str = "float dist" + i + " = sdTriPrism(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), float2(" + this.h.x + ", " + this.h.y + "));";
            return str;
        }
    }
}