using UnityEngine;
using System;
using SDF;
using System.Collections.Generic;


namespace SDF
{
    public class SphereNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public float s;
    }
}

