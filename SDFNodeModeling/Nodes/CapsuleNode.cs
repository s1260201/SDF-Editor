using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF.Model
{
    public class CapsuleNode : SDFObjNode
    {
        public Vector3 p;
        public float h;
        public float r;
        public override string Calcsd(int i)
        {
            Debug.Log("Write a CapsuleNode");
            string str = "float dist" + i + " = sdVerticalCapsule(float3(pos.x - " + this.p.x + ",pos.y - " + this.p.y + ",pos.z - " + this.p.z + ")," + this.h + "," + this.r + ");";
            return str;
        }
    }
}