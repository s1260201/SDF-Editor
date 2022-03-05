using UnityEngine;

namespace SDF.General
{
    public class Timer : XNode.Node
    {
        [Input] public float start = 0;
        [Output] public float t;

        public override object GetValue(XNode.NodePort port)
        {
            start = GetInputValue<float>("Start", this.start);

            t = Time.time + start;
            return t;
        }
    }
}

