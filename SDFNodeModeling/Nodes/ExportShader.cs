using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        public List<SDFObj> list;
        string path = "Assets/Shader/src/Template.shader";
        // Update is called once per frame
        void Start()
        {
            /*
            list = sdfGraph.OutputNode();
            Debug.Log(list);
            */
            // Test list
            list = new List<SDFObj>();
            Vector3 pos = new Vector3(0,0,0);
            Sphere sphere = new Sphere(pos,2);
            list.Add(sphere);
            int num = 1;
            try
            {
                Regex reg = new Regex("// SDF");
                StreamReader streamReader = new StreamReader(path);
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (reg.Match(line).Success)
                    {
                        StreamWriter streamWriter = new StreamWriter(path);
                        streamWriter.WriteLine("float marchingDist = sdSphere(pos,0.5);");
                        Debug.Log("Clear");
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError("The file could not be read");
            }
            
        }
    }
}

