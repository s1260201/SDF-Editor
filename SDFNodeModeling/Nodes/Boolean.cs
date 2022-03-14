using UnityEngine;
using System;

namespace SDF.Controll
{
    public class Boolean : XNode.Node
    {
        public BoolType boolType = BoolType.Union;
        public enum BoolType { Union, Intersection, Difference }

        [Input] public float a;
        [Output] public float t;

        public override object GetValue(XNode.NodePort port)
        {
            switch (boolType)
            {
                case BoolType.Union: default: return 0;
                case BoolType.Intersection: return 1;
                case BoolType.Difference: return 2;
            }
        }

        private float Union()
        {
            return 0;
        }
    }
}