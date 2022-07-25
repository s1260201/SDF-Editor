using UnityEngine;
namespace SDF.Controll.Bool
{
    public class DifferenceNode : SDFBoolNode
    {
        [Output] public SDFNode targetNodes;
        [Output] public SDFNode negativeNodes;

        public override string CalcBool(int a, int b, int i)
        {
            return "float dist" + i + " = max(-dist" + a + ", dist" + b + ");";
        }
    }
}

