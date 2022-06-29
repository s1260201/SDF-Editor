using UnityEngine;

namespace SDF.Model
{
    public class TorusNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public Vector2 t;

        public override string calcsd(int i)
        {
            Debug.Log("Write a TorusNode");
            string str = "float dist" + i + " = sdTorus(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), float2(" + this.t.x + ", " + this.t.y + "));";
            return str;
        }
    }
}