using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class SphereNode : XNode.Node
    {
        [Input(ShowBackingValue.Never)] public List<SDFObj> inputObjectList;
        [Input] public Vector3 p;
        [Input] public float s;
        [Output] public List<SDFObj> outputObjectList;


        public override object GetValue(XNode.NodePort port)
        {
            outputObjectList = GetInputValue<List<SDFObj>>("List",this.inputObjectList);
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);
            Sphere obj = new Sphere(p, s); ;
            if (outputObjectList == null)
            {
                List<SDFObj> list = new List<SDFObj>();
            }
            outputObjectList.Add(obj);
            Debug.Log("Count");
            return outputObjectList;
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

