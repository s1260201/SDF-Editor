using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class SphereNode : SDFNode
    {
        [Input] public Vector3 p;
        [Input] public float s;
        
        /*
        public override SDFNode getNode()
        {
            if(nextNode == null)
            {
                objList = new List<SDFObj>();
            }
            else
            {
                nextNode.getNode();
                objList = nextNode.objList;
            }
            Sphere obj = new Sphere();
            obj.s = this.s;
            objList.Add(obj);
            Debug.Log("Add obj to list");
            return nextNode;
        }
        */

        /*
        public override object GetValue(XNode.NodePort port)
        {
            p = GetInputValue<Vector3>("p", this.p);
            s = GetInputValue<float>("s", this.s);

            sd = p.magnitude - s;
            return sd;
        }
        */
        public override SDFObj addObj()
        {
            return new Sphere();
        }
    }
}

