using UnityEngine;

namespace SDF.Controll
{
    public class RepeatNode : SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Input] public float interval = 50.0f;
        public Plane plane = Plane.xy;
        public enum Plane { xy,yz,xz}
        [Output] public SDFNode node;
    }

}