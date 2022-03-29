using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF
{
    public class SDFObjNode : SDFNode
    {
        [Output] public int nextNode;
        public override object GetValue(XNode.NodePort port)
        {
            beforeNode = GetInputValue<int>("beforeNode", this.beforeNode);
            nextNode = beforeNode + 1;
            order = nextNode;
            return nextNode;
        }
    }
}

