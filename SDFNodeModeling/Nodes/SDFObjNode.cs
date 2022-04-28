using UnityEngine;

namespace SDF
{
	public class SDFObjNode : SDFNode
	{
		[Input(ShowBackingValue.Never)] public SDFNode beforeNode;
	}
}
