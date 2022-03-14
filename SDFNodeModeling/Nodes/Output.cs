using UnityEngine;
using System;

namespace SDF.Controll
{
    public class Output: XNode.Node
    {
        [Input(ShowBackingValue.Never)] public float Value;

        /*
        public override object GetValue(XNode.NodePort port)
        {

        }
        */
        public float OutputSD()
        {
            float sd = GetInputValue<float>("Value", this.Value);
            return sd;
        }
    }
}