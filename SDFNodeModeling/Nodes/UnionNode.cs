using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class UnionNode : Operater
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output (dynamicPortList =true)] public List<SDFNode> nodes;

        public int listCounter()
        {
            return this.nodes.Count;
        }
    }
    
}