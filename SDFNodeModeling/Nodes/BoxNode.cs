using UnityEngine;

namespace SDF.Model
{
    public class BoxNode : SDFObjNode
    { 
        [Input] public Vector3 p;
        [Input] public Vector3 b = new Vector3(1,1,1);   
    }
}

