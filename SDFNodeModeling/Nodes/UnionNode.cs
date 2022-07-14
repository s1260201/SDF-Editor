using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class UnionNode : SDFOperate
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Output (dynamicPortList =true)] public List<SDFNode> nodes;

        public int listCounter()
        {
            return this.nodes.Count;
        }
        /*
        public override string CalcOpe(int i)
        {
            int a = this.listCounter();
            string str = "float dist" + i + " = ";
            for (int j = 0; j < a - 1; j++)
            {
                str += "min(";
            }
            streamWriter.Write("dist" + taskStack.Pop());
            for (int j = 1; j < a; j++)
            {
                streamWriter.Write(",dist" + taskStack.Pop() + ")");
            }
            streamWriter.WriteLine(";");
            return str;
        }
        */
    }
    
}