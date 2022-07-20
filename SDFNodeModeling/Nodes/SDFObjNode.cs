using UnityEngine;
using System.IO;

namespace SDF
{
	[NodeTint("#fcba03")]
	public class SDFObjNode : SDFNode
	{
		[Input(ShowBackingValue.Never)] public SDFNode beforeNode;
		public virtual string Calcsd(int i)
		{
			return "";
		}
	}
}
