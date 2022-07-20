using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using SDF;

[NodeTint("#00ff00")]
public class SDFBoolNode : SDFNode {
    [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
    
    public virtual string CalcBool(int a, int b, int i)
    {
        return "";
    }
}