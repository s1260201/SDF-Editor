using UnityEngine;
using XNode;
using System;
using SDF.Controll;


namespace SDFModeling
{
    [Serializable, CreateAssetMenu(fileName = "RaymaGraph", menuName = "Node Graph/RaymaGraph")]
    public class SDFGraph : XNode.NodeGraph
    {
        public float OutputNode()
        {
            Output result = null;
            foreach (var node in nodes)
            {
                result = node as Output;
                if (result != null)
                {
                    break;
                }
            }
            float value = result.OutputSD();
            return value;
        }
    }
}

