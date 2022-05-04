using UnityEngine;

namespace SDF
{
	public class SDFObjNode : SDFNode
	{
		[Output(ShowBackingValue.Never)] public SDFNode nextNode;
	}
}
