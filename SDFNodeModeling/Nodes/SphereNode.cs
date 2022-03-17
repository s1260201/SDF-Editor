using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class SphereNode : XNode.Node
    {
        [Input] public List<SDFObj> inputObject;
        [Input] public Vector3 p;
        [Input] public float s;
        [Output] public List<SDFObj> outputObject;

        public override object GetValue(XNode.NodePort port)
        {
            outputObject = GetInputValue<List<SDFObj>>("List",this.inputObject);
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            Sphere obj = new Sphere(p, s);

            outputObject.Add(obj);

            return outputObject;
        }

        /*
        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            sd = p.magnitude - s;
            return sd;
        }
        */
    }
}

