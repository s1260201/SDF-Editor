using UnityEngine;

namespace SDF.Controll
{
    public class BlendingNode : SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode a;
        [Output] public SDFNode b;
    }

}