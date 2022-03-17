using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        public List<SDFObj> list;
        // Update is called once per frame
        void Start()
        {
            list = sdfGraph.OutputNode();
            Debug.Log(list);
        }
    }
}

