using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDFModeling
{
    public class GetValue : MonoBehaviour
    {
        [SerializeField] SDFGraph _graph;

        private void ShowValue()
        {
            float value = _graph.OutputNode();
            Debug.Log(value);
        }

        private void Update()
        {
            float value = _graph.OutputNode();
            Debug.Log(value);
        }

    }
}

