using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;

namespace SDF.Model
{
    public class RoundBoxNode : XNode.Node
    {
        [Input] public List<SDFObj> inputObject;

        [Input] public Vector3 p;
        [Input] public Vector3 b;
        [Input] public float r;
        [Output] public List<SDFObj> outputObject;


        public override object GetValue(XNode.NodePort port)
        {
            outputObject = GetInputValue<List<SDFObj>>("List", this.inputObject);
            p = GetInputValue<Vector3>("p", this.p);
            b = GetInputValue<Vector3>("b", this.b);
            r = GetInputValue<float>("r", this.r);
            /*
            Vector3 q;
            q.x = Math.Max(Math.Abs(p.x) - b.x, 0.0f);
            q.y = Math.Max(Math.Abs(p.y) - b.y, 0.0f);
            q.z = Math.Max(Math.Abs(p.z) - b.z, 0.0f);
            return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f) - r;
            */
            //RoundBox obj = new RoundBox(p, b, r);
            RoundBox obj = new RoundBox();
            obj.pos = p;
            obj.b = this.b;
            obj.r = this.r;
            outputObject.Add(obj);
            return outputObject;
        }
    }
}

