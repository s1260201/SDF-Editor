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
        [SerializeField] string readPath = "Assets/Shader/src/Sample.shader";
        [SerializeField] string writePath = "Assets/Shader/Export/Sample1.shader";
        [SerializeField] string ShaderName = "Sample";

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

            OutputShader();
            
            
        }

        public void OutputShader()
        {
            try
            {
                Regex reg = new Regex("// SDF");
                StreamReader streamReader = new StreamReader(readPath);
                StreamWriter streamWriter = new StreamWriter(writePath);
                string line = null;

                while ((line = streamReader.ReadLine()) != null)
                {
                    //Debug.Log(line);
                    if (reg.Match(line).Success)
                    {
                        Debug.Log("Match");

                        /*
                        while(list[i]!=null){
                        switch (list[i].GetType())
                        {
                            case Sphere:
                                streamWriter.WriteLine("float marchingDist = sdSphere(pos,0.5);");
                                break;
                            case Box:
                                break;
                            case Plane:
                                break;
                        }
                        
                        }
                        */
                        streamWriter.WriteLine("float marchingDist = sdSphere(pos,0.5);");
                        streamWriter.Flush();
                        Debug.Log("Clear");
                    }
                    else
                    {
                        //Debug.Log(line);
                        streamWriter.WriteLine(line);
                        streamWriter.Flush();
                    }
                }
                streamReader.Close();
                streamWriter.Close();

            }
            catch (Exception e)
            {
                Debug.LogError("The file could not be read");
            }
        }
    }
}

