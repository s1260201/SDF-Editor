using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;
using XNode;


namespace SDF.Model
{
    public class SphereNode : SDFNode
    {
        [Input(ShowBackingValue.Never)] public SDFNode inputNode;
        [Input] public Vector3 p;
        [Input] public float s;
        [Output] public SDFNode outputNode;

        
        public override object GetValue(XNode.NodePort port)
        {
            outputNode = GetInputValue<SDFNode>("List",this.inputNode);
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            return outputNode;
        }
        
        public override List<SDFObj> OutputList()
        {
            Sphere sphere = new Sphere(this.p, this.s);
            List<SDFObj> objList;
            if (inputNode == null)
            {
                objList = new List<SDFObj>();
            }
            else
            {
                objList = inputNode.OutputList();
            }
            objList.Add(sphere);
            return objList;

        }
    }
}

