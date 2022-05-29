using UnityEngine;

namespace SDF
{
	public class SDFObjNode : SDFNode
	{
		[Input(ShowBackingValue.Never)] public SDFNode beforeNode;
		//[Input] public Vector3 position;
	}
}
