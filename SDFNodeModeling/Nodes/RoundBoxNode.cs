using UnityEngine;

namespace SDF.Model
{
    public class RoundBoxNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public Vector3 b = new Vector3(1,1,1);
        [Input] public float r;

        public override string calcsd(int i)
        {
            Debug.Log("Write a RoundBox code");
            string str = "float dist" + i + " = sdRoundBox(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), float3(" + this.b.x + "," + this.b.y + "," + this.b.z + ")," + this.r + ");";
            return str;
        }
    }
}

