using UnityEngine;
using XNode;
using System;
using SDF.Controll;
using System.Collections.Generic;
using SDF;


namespace SDFModeling
{
    [Serializable, CreateAssetMenu(fileName = "RaymaGraph", menuName = "Node Graph/RaymaGraph")]
    public class SDFGraph : XNode.NodeGraph
    {
        public SDFNode current;
        public SDFNode HeadNode()
        {
            Head result = null;
            foreach (var node in nodes)
            {
                result = node as Head;
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }
    }
}

