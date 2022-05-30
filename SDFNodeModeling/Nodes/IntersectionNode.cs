using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class IntersectionNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode aNodes;
        [Output] public SDFNode bNodes;

    }

}