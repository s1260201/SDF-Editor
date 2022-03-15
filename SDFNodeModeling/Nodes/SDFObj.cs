using UnityEngine;
using System;


namespace SDF
{
    public class SDFObj : MonoBehaviour
    {
        public Vector3 pos;
    }

    public class Sphere : SDFObj
    {
        float s;

        public Sphere()
        {
            float Sdf()
            {
                return SdSphere();
            }
            float SdSphere()
            {
                return pos.magnitude - s;
            }
            void getValue(Vector3 pos, float s)
            {
                this.pos = pos;
                this.s = s;
            }
        }
    }

    public class Box : SDFObj
    {
        protected Vector3 b;

        public Box()
        {
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
            void getValue(Vector3 pos, Vector3 b)
            {
                this.pos = pos;
                this.b = b;
            }
        }
    }

    public class RoundBox : SDFObj
    {
        protected Vector3 b;
        float r;

        public RoundBox()
        {
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

                return q.magnitude + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0.0f) - r;
            }
            void getValue(Vector3 pos, Vector3 b, float r)
            {
                this.pos = pos;
                this.b = b;
                this.r = r;
            }
        }
    }
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
