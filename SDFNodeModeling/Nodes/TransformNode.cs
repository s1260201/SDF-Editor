using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDF;

namespace SDF.Controll
{
    public class TransformNode : SDFOperate
    {
        [Input] public SDFNode beforeNode;
        public Vector3 nodePosition;
        public Vector3 nodeRotate;
        public Vector3 nodeScale = new Vector3(1, 1, 1);
        [Output] public SDFNode node;

        public override string CalcOpe()
        {
            string str = "";
            str += "pos = float3(pos.x - " + this.nodePosition.x + ",pos.y - " + this.nodePosition.y + ",pos.z - " + this.nodePosition.z + ");\n";
            if(this.nodeRotate.z != 0)
                str += "pos.xy = rot(pos.xy," + this.nodeRotate.z + ");\n";
            if (this.nodeRotate.x != 0)
                str += "pos.yz = rot(pos.yz," + this.nodeRotate.x + ");\n";
            if (this.nodeRotate.y != 0)
                str += "pos.xz = rot(pos.xz," + this.nodeRotate.y + ");\n";
            if (this.nodeScale.x != 0)
                str += "pos.x *= " + 1 / this.nodeScale.x + ";\n";
            if (this.nodeScale.y != 0)
                str += "pos.y *= " + 1 / this.nodeScale.y + ";\n";
            if (this.nodeScale.z != 0)
                str += "pos.z *= " + 1 / this.nodeScale.z + ";\n";
            return str;
        }
    }
}


