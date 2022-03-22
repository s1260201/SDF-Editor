using UnityEngine;
using System;
using System.Collections.Generic;
using SDF;
using XNode;

namespace SDF.Controll
{
    public class Output: SDFNode
    {
        public ActionType actionType = ActionType.Preview;
        public enum ActionType { Preview, Render }

        [Input(ShowBackingValue.Never)] public SDFNode inputNode;

        public override object GetValue(XNode.NodePort port)
        {
            SDFNode inputNode = GetInputValue<SDFNode>("InputNode", this.inputNode);
            switch (actionType)
            {
                case ActionType.Preview:
                default:
                    return 0;
                case ActionType.Render:
                    return 1;
            }
        }
        
        public override List<SDFObj> OutputList()
        {
            //SDFNode parent = GetInputValue<SDFNode>("InputNode", this.inputNode);
            //SDFNode parent = this.inputNode;
            if (this.inputNode == null) Debug.Log("parent is null.");
            List<SDFObj> objList;
            objList= inputNode.OutputList();
            return objList;
        }
        
    }
}