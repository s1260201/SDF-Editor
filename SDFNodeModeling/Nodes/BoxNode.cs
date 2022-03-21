using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class BoxNode : SDFNode
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

            Box obj = new Box(p, b);
            outputObject.Add(obj);
            return outputObject;
        }
    }
}

