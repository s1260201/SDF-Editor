using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class IntersectionNode : SDFBoolNode
    {
        [Output] public SDFNode aNodes;
        [Output] public SDFNode bNodes;
        public override string CalcBool(int a, int b, int i)
        {
            return "float dist" + i + " = max(dist" + a + ", dist" + b + ");";
        }
    }

}