using UnityEngine;

namespace SDF.Controll
{
    public class SmoothIntersectionNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode aNodes;
        [Output] public SDFNode bNodes;
    }

}