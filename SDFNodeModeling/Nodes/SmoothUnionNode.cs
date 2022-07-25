using UnityEngine;

namespace SDF.Controll
{
    [NodeTint("#ccffcc")]
    public class SmoothUnionNode: SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode a;
        [Output] public SDFNode b;
    }

}