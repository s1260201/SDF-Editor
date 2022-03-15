using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using SDF.Controll;

public class GetObjList : MonoBehaviour
{
    [SerializeField] SDFGraph sdfGraph;
    // Update is called once per frame
    void Start()
    {
        foreach (var node in sdfGraph.nodes)
        {
            Debug.Log(node);
        }
    }
}
