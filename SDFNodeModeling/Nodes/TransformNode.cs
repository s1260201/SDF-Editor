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
            string str = "pos = original_pos;\n";
            str += "pos = float3(pos.x - " + this.nodePosition.x + ",pos.y - " + this.nodePosition.y + ",pos.z - " + this.nodePosition.z + ");\n";
            str += "pos.xy = rot(pos.xy," + this.nodeRotate.z + ");\n";
            str += "pos.yz = rot(pos.yz," + this.nodeRotate.x + ");\n";
            str += "pos.xz = rot(pos.xz," + this.nodeRotate.y + ");\n";
            str += "pos.x *= " + 1 / this.nodeScale.x + ";\n";
            str += "pos.y *= " + 1 / this.nodeScale.y + ";\n";
            str += "pos.z *= " + 1 / this.nodeScale.z + ";\n";
            return str;
        }

    }
}


