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

        [Input(ShowBackingValue.Never)] public Node inputNode;

        public override object GetValue(XNode.NodePort port)
        {
            switch (actionType)
            {
                case ActionType.Preview:
                default:
                    return 0;
                case ActionType.Render:
                    return 1;
            }
        }
        public Node OutputList()
        {
            return GetInputValue<Node>("Node",this.inputNode);
        }
    }
}