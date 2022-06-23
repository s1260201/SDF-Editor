using UnityEngine;

namespace SDF.Controll.Bool
{
    public class SmoothDifferenceNode : SDFNode
    {
        [Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output] public SDFNode targetNodes;
        [Output] public SDFNode negativeNodes;
        /*
        public float calcsd()
        {

        }
        */
    }
}
