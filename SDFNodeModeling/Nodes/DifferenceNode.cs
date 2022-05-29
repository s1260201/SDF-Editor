using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SDF.Controll.Bool
{
    public class DifferenceNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode targetNodes;
        //[Output(dynamicPortList = true)] public List<SDFNode> nodes;
        [Output] public SDFNode negativeNodes;
    }
}

