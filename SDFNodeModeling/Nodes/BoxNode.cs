using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class BoxNode : XNode.Node
    {
        [Input] public List<SDFObj> inputObject;
        [Input] public Vector3 p;
        [Input] public Vector3 b;
        [Output] public List<SDFObj> outputObject;

        public override object GetValue(XNode.NodePort port)
        {
            outputObject = GetInputValue<List<SDFObj>>("List", this.inputObject);
            p = GetInputValue<Vector3>("p", this.p);
            b = GetInputValue<Vector3>("b", this.b);

            /*
            Vector3 q;
            q.x = Math.Max(Math.Abs(p.x) - b.x, 0.0f);
            q.y = Math.Max(Math.Abs(p.y) - b.y, 0.0f);
            q.z = Math.Max(Math.Abs(p.z) - b.z, 0.0f);
            sd = q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f);
            */

            //Box obj = new Box(p, b);
            Box obj = new Box();
            obj.pos = p;
            obj.b = this.b;
            outputObject.Add(obj);
            return outputObject;
        }
    }
}

