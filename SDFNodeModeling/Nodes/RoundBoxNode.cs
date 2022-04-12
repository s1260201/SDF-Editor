using UnityEngine;

namespace SDF.Model
{
    public class RoundBoxNode : SDFObjNode
    {
        [Input] public Vector3 p;
        [Input] public Vector3 b;
        [Input] public float r;
    }
}

