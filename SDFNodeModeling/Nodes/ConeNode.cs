using UnityEngine;
using XNode;

namespace SDF.Model
{
	public class ConeNode : SDFObjNode
	{
		[Input] public Vector3 p;
		[Input] public Vector2 c;
		[Input] public float h;
	}
}