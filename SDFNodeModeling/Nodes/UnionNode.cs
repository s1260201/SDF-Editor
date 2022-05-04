using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class UnionNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode nodes;
    }
}