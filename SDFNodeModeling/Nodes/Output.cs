using UnityEngine;
using System;
using System.Collections.Generic;
using SDF;

namespace SDF.Controll
{
    public class Output: XNode.Node
    {
        public ActionType actionType = ActionType.Preview;
        public enum ActionType { Preview, Render }

        [Input(ShowBackingValue.Never)] public List<SDFObj> inputObject;

        public override object GetValue(XNode.NodePort port)
        {
            switch (actionType)
            {
                case ActionType.Preview:
                default:
                    return 0;
                case ActionType.Render:
                    List<SDFObj> objList = new List<SDFObj>();
                    return 1;
            }
        }
        public List<SDFObj> OutputList()
        {
            return GetInputValue<List<SDFObj>>("List",this.inputObject);
        }
    }
}