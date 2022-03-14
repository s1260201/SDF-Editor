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
                case ActionType.Preview: default:
                    return 0;
                case ActionType.Render:
                    return 1;
                    // https://naoyu.dev/%E3%80%90unity%E3%80%91%E8%87%AA%E4%BD%9C%E3%82%AF%E3%83%A9%E3%82%B9%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E3%81%BF%E3%82%88%E3%81%86%E3%80%90c%E3%80%91/
            }
        }
    }
}