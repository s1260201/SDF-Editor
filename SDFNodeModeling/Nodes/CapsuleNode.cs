using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF.Model
{
	public class CapsuleNode : SDFObjNode
	{
		[Input] public Vector3 p;
		[Input] public float h;
		[Input] public float r;
	}
}