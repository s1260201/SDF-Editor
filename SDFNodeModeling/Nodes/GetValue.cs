using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDF;

namespace SDFModeling
{
    public class GetValue : MonoBehaviour
    {
        [SerializeField] SDFGraph _graph;
        /*
        private void ShowValue()
        {
            List<SDFObj> value = _graph.OutputNode();
            Debug.Log(value);
        }
        */
    }
}