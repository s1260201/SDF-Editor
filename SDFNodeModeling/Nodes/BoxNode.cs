using UnityEngine;

namespace SDF.Model
{
    public class BoxNode : SDFObjNode
    { 
        public Vector3 p;
        public Vector3 b = new Vector3(1,1,1);

        public override string Calcsd(int i)
        {
            Debug.Log("Write a Box code");
            string str = "float dist" + i + " = sdBox(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), float3(" + this.b.x + "," + this.b.y + "," + this.b.z + "));";
            return str;
        }
    }
}

