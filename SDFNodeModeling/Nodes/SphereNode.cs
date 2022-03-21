using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;
using XNode;


namespace SDF.Model
{
    public class SphereNode : SDFNode
    {
        [Input(ShowBackingValue.Never)] public Node inputNode;
        [Input] public Vector3 p;
        [Input] public float s;
        [Output] public Node outputNode;

        public override object GetValue(XNode.NodePort port)
        {
            outputNode = GetInputValue<Node>("List",this.inputNode);
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            return outputNode;
        }
    }
}

