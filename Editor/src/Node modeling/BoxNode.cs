using UnityEngine;
using System;

public class Vector : XNode.Node
{
    [Input] public Vector3 p;
    [Input] public Vector3 b;
    [Output] public float sd;

    public override object GetValue(XNode.NodePort port)
    {
        Vector3 q;
        q.x = Math.Max(Math.Abs(p.x) - b.x, 0.0f);
        q.y = Math.Max(Math.Abs(p.y) - b.y, 0.0f);
        q.z = Math.Max(Math.Abs(p.z) - b.z, 0.0f);

        return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y,q.z)),0.0f);
    }
}