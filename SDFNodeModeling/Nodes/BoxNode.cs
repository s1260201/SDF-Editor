using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF.Model
{
    public class BoxNode : XNode.Node
    {
        [Input] public List<SDFObj> beforeNode;
        [Input] public Vector3 p;
        [Input] public Vector3 b;
        [Output] public List<SDFObj> nextNode;
    
    }
}

