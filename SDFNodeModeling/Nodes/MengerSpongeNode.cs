using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace SDF.Model
{
	public class MengerSpongeNode : SDFObjNode
	{
		public Vector3 p;
		public Vector3 b = new Vector3(1, 1, 1);
		public float scale = 50;

		public override string Calcsd(int i)
		{
			Debug.Log("Write a MengerSponge code.");
			string str = "float dist" + i + " = sdMengerSponge(float3(pos.x - " + this.p.x + ", pos.y -  " + this.p.y + ", pos.z - " + this.p.z + "), float3(" + this.b.x + "," + this.b.y + "," + this.b.z + "), " + this.scale + ");";
			return str;
		}
	}
}