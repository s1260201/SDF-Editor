using UnityEngine;
using System;

namespace RayMarching
{
    public class Sphere : XNode.Node
    {
        [Input] public Vector3 p;
        [Input] public float s;
        [Output] public float sd;

        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            sd = p.magnitude - s;
            return sd;
        }
    }
}

