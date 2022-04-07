using UnityEngine;
using System;
using System.Collections.Generic;
using XNode;


namespace SDF
{
    public class SphereNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public float s;
    }
}

