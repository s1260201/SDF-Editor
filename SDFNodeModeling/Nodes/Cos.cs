using UnityEngine;

namespace SDF.General
{
    public class Cos : XNode.Node
    {
        [Input] public float x;
        [Output] public float f;

        public override object GetValue(XNode.NodePort port)
        {
            x = GetInputValue<float>("X", this.x);

            f = Mathf.Cos(x);
            return f;
        }
    }
}

