using UnityEngine;
using System;

namespace RayMarching
{
    public class Plane : XNode.Node
    {
        [Input] public Vector3 p;
        [Input] public Vector3 n;
        [Input] public float h;
        [Output] public float sd;

        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            n = GetInputValue<Vector3>("n", this.n);
            h = GetInputValue<float>("h", this.h);

            return Vector3.Dot(p,n.normalized) + h;
        }
    }
}

