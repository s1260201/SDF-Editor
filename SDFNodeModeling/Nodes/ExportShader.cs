using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using System.Collections.Generic;
using UnityEngine.UI;
using XNode;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        private int index;


        // Update is called once per frame
        void Start()
        {
            var objNode = sdfGraph.nodes;
            Debug.Log(objNode);
    }
    }
}

