using UnityEngine;
using System;

namespace SDF.Controll
{
    public class Head : XNode.Node
    {
        public ActionType actionType = ActionType.Preview;
        public enum ActionType { Preview, Render }

        [Output] public float a;

        public override object GetValue(XNode.NodePort port)
        {
            switch (actionType)
            {
                case ActionType.Preview: default: return 0;
                case ActionType.Render: return 1;
            }
        }
    }
}