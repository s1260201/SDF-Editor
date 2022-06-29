using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll
{
    public class TransformNode : SDFNode
    {
        [Input] public SDFNode beforeNode;
        [Input] public Vector3 nodePosition;
        [Input] public Vector3 nodeRotate;
        [Input] public Vector3 nodeScale;

        public Vector3 calcPos(Vector3 pos)
        {
            pos.x = pos.x + this.nodePosition.x;
            pos.y = pos.y + this.nodePosition.y;
            pos.z = pos.z + this.nodePosition.z;

            return pos;
        }

    }
}


