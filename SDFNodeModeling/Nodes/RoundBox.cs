using UnityEngine;
using System;

namespace SDF.Model
{
    public class RoundBox : XNode.Node
    {
        [Input] public Vector3 p;
        [Input] public Vector3 b;
        [Input] public float r;
        [Output] public float sd;

        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            b = GetInputValue<Vector3>("b", this.b);
            r = GetInputValue<float>("r", this.r);
            Vector3 q;
            q.x = Math.Max(Math.Abs(p.x) - b.x, 0.0f);
            q.y = Math.Max(Math.Abs(p.y) - b.y, 0.0f);
            q.z = Math.Max(Math.Abs(p.z) - b.z, 0.0f);

            return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f) - r;
            
        }
    }
}

