using UnityEngine;
using System;

namespace RayMarching
{
    public class Torus : XNode.Node
    {
        [Input] public Vector3 p;
        [Input] public Vector2 t;
        [Output] public float sd;

        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            t = GetInputValue<Vector2>("t", this.t);

            Vector2 q;
            q.x = p.x;
            q.y = p.z;
            q.x = q.magnitude - t.x;
            q.y = p.y;
            sd = q.magnitude - t.y;

            return sd;
        }
    }
}