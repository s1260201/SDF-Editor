using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll
{
    public class TransformNode : SDFNode
    {
        [Input] public SDFNode beforeNode;
        public Vector3 nodePosition;
        public Vector3 nodeRotate;
        public Vector3 nodeScale = new Vector3(1, 1, 1);
        [Output] public SDFNode node;

        public Vector3 calcPos(Vector3 pos)
        {
            pos.x = pos.x + this.nodePosition.x;
            pos.y = pos.y + this.nodePosition.y;
            pos.z = pos.z + this.nodePosition.z;

            return pos;
        }

    }
}


