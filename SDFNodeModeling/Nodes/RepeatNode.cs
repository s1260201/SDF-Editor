using UnityEngine;

namespace SDF.Controll
{
    public class RepeatNode : SDFOperate
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        public float interval = 50.0f;
        public bool x, y, z;
        [Output] public SDFNode node;

        
        public override string CalcOpe()
        {
            Debug.Log("Write a repeat code");
            string str = "";
            string repPlane = this.RepPlane();
            str += "pos." + repPlane + " = repeat(pos." + repPlane + ", " + this.interval + ");";
            return str;
        }
        

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