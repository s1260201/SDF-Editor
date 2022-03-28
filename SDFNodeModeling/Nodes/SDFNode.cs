using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF
{
    public class SDFNode : Node
    {
        [Input(ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode nextNode;

        public virtual SDFObj addObj()
        {
            return null;
        }
    }
}