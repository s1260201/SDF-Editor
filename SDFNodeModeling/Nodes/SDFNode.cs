using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF
{
    public class SDFNode : Node
    {
        public int order;
        [Input(ShowBackingValue.Never)] public int beforeNode = 0;
    }
}