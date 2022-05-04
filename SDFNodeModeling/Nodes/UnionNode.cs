using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll
{
    public class UnionNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode nodes;
        [Output] public SDFNode nextNode;
    }
}
