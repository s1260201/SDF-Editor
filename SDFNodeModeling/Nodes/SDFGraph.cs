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
        public Node GetOutputNode()
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
            return result;
        }
        public List<SDFNode> GetNode()
        {
            List<SDFNode> nodeList = new List<SDFNode>();
            for(int i = 0; i < nodes.Count; i++)
            {
                nodeList.Add((SDFNode)nodes[i]);

            }
            return nodeList;         
        }
    }
}

