using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll.Bool
{
    public class UnionNode : SDFBoolNode
    {
        [Output(dynamicPortList = true)] public List<SDFNode> nodes;
        public int listCounter()
        {
            return this.nodes.Count;
        }
        
        public override string CalcBool(int a, int b, int i)
        {
            int c = this.listCounter();
            string str = "float dist" + i + " = ";
            for (int j = 0; j < c - 1; j++)
            {
                str += "min(";
            }
            str += "dist" + a;
            for (int j = 1; j < c; j++)
            {
                str += ",dist" + b + ")";
            }
            str += ";";
            return str;
        }
        
    }
    
}