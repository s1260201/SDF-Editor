using System;
using UnityEngine;


namespace SDF
{
    public class SDFObj : MonoBehaviour
    {
        public Vector3 pos;
    }

    /*
    public class Sphere : SDFObj
    {
        float s;


        public Sphere(Vector3 pos, float s)
        {
            this.pos = pos;
            this.s = s;

        }
               float Sdf()
            {
                return SdSphere();
            }
            float SdSphere()
            {
                return pos.magnitude - s;
            }  
    }
    */

    public class Sphere : SDFObj
    {
        public float s;
    }

    public class Box : SDFObj
    {
        public Vector3 b;
    }
    public class RoundBox : SDFObj
    {
        public Vector3 b;
        public float r;
    }


    /*
    public class Box : SDFObj
    {
        public Vector3 b;

        public Box(Vector3 pos, Vector3 b)
        {
            this.pos = pos;
            this.b = b;

            float Sdf()
            {
                return SdBox();
            }
            float SdBox()
            {
                Vector3 q = pos - b;
                q.x = Math.Max(q.x, 0.0f);
                q.y = Math.Max(q.y, 0.0f);
                q.z = Math.Max(q.z, 0.0f);

                return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f);
            }
        }
    }
    */
    /*
    public class RoundBox : SDFObj
    {
        public Vector3 b;
        public float r;

        public RoundBox(Vector3 pos, Vector3 b, float r)
        {
            this.pos = pos;
            this.b = b;
            this.r = r;

            float Sdf()
            {
                return SdRoundBox();
            }
            float SdRoundBox()
            {
                Vector3 q = pos - b;
                q.x = Math.Max(q.x, 0.0f);
                q.y = Math.Max(q.y, 0.0f);
                q.z = Math.Max(q.z, 0.0f);

                return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f) - r;
            }
        }
    }
    */
    /*
    public class RoundBox : Box
    {
        float r;
        
        public RoundBox()
        {
            float Sdf()
            {
                return SdBox() - r;
            }
            void getValue(Vector3 pos, Vector3 b, float r)
            {
                this.pos = pos;
                this.b = b;
            }
        }
    }
    */
}
