using UnityEngine;
using System;
using System.Collections.Generic;
using SDF;

namespace SDF
{
    public class Output: SDFNode
    {
        //public ActionType actionType = ActionType.Preview;
        //public enum ActionType { Preview, Render }

        /*
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
        */
        /*
        public List<SDFObj> getAllNode()
        {
            SDFNode node = GetInputValue<SDFNode>("Node", this.inputNode);
            SDFNode unionObj = node.getNode();
            List<SDFObj> list = unionObj.objList;
            return list;
        }
        */
        /*
        public List<SDFObj> OutputList()
        {
            getNode();
            return GetInputValue<List<SDFObj>>("List",this.inputObject);
        }
        */
        /*
        public SDFNode outputNodes()
        {
            return beforeNode;
        }
        */
    }
}