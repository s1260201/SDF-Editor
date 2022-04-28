using UnityEngine;
using System;

namespace SDF.Controll
{
    public class Boolean : XNode.Node
    {
        public BoolType boolType = BoolType.Union;
        public enum BoolType { Union, Intersection, Difference }

        [Input(connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)] public SDFNode baseNode;
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode othersNodes;
        [Output] public SDFNode outputNode;
    }
}