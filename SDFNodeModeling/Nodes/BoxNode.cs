using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF
{
    public class BoxNode : SDFObjNode
    { 
        [Input] public Vector3 p;
        [Input] public Vector3 b;   
    }
}

