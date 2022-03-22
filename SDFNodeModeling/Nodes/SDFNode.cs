using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace SDF
{
    public class SDFNode : Node
    {
        // Start is called before the first frame update
        public SDFNode GetParents(SDFNode parentNode)
        {
            return this.GetParents(parentNode);
        }
        /*
        public List<SDFObj> OutputList(SDFNode parent)
        {
            List<SDFObj> objList = parent.OutputList(parent);
            return objList;
        }
        */
        public virtual List<SDFObj> OutputList()
        {
            List<SDFObj> objList = new List<SDFObj>();
            return objList;
        }
    }
}