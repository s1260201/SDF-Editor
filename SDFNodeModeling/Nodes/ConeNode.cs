using UnityEngine;
using System;
using XNode;

namespace SDF.Model
{
	public class ConeNode : SDFObjNode
	{
		[Input] public Vector3 p;
		[Input] public float h;
		[SerializeField, Range(0.0f, 90.0f)]
		[Input] public float angle;

		public Vector2 triangle(float angle)
        {
			float sin = (float)Math.Sin(angle * (Math.PI / 180));
			float cos = (float)Math.Cos(angle * (Math.PI / 180));
			return new Vector2(sin, cos);
		}

		public override string Calcsd(int i)
        {
			Debug.Log("Write a ConeNode");
			Vector2 c = this.triangle(this.angle);
			string str = "float dist" + i + " = sdCone(float3(pos.x - " + this.p.x + ",pos.y - " + this.p.y + ",pos.z - " + this.p.z + "),float2(" + c.x + "," + c.y + ")," + this.h + ");";
			return str;

		}
	}

}