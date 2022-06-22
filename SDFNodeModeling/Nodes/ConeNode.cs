using UnityEngine;
using System;
using XNode;

namespace SDF.Model
{
	public class ConeNode : SDFObjNode
	{
		[Input] public Vector3 p;
		//[Input] public Vector2 c;	
		[Input] public float h;
		[SerializeField, Range(0.0f, 90.0f)]
		[Input] public float angle;

		public Vector2 triangle(float angle)
        {
			float sin = (float)Math.Sin(angle * (Math.PI / 180));
			float cos = (float)Math.Cos(angle * (Math.PI / 180));
			return new Vector2(sin, cos);
		}
	}

}