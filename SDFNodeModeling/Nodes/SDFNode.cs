using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF
{
    public class SDFNode : Node
    {
        [Input(ShowBackingValue.Never)] public SDFNode beforeNode;
        [Input(ShowBackingValue.Never)] public int obOrder = 0;
        [Output] public SDFNode nextNode;

        int nodeNum = 0;

        public virtual SDFObj addObj()
        {
            return null;
        }
        public SDFNode getBeforeNode()
        {
            return this.beforeNode;
        }

    }
}