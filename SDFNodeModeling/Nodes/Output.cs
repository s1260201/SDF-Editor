using UnityEngine;

namespace SDF
{
    public class Output: SDFNode
    {
        [Input(ShowBackingValue.Never)] public SDFNode beforeNode;
    }
}