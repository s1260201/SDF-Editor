using UnityEngine;

namespace SDF.Controll
{
    public class RepeatNode : SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Input] public float interval = 50.0f;
        public bool x, y, z;
        [Output] public SDFNode node;


        public string RepPlane()
        {
            if (x)
            {
                if (y)
                {
                    if (z)
                    {
                        return "xyz";
                    }
                    else
                    {
                        return "xy";
                    }
                }
                else if(z)
                {
                    return "xz";
                }
                else
                {
                    return "x";
                }
            }
            else
            {
                if (y)
                {
                    if (z)
                    {
                        return "yz";
                    }
                    else
                    {
                        return "y";
                    }
                }
                else
                {
                    return "z";
                }
            }
        }
    }


}