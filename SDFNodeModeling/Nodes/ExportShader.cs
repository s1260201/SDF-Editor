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
        string readPath = "Assets/Shader/src/Sample.shader";
        string writePath = "Assets/Shader/Export/Sample1.shader";

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
            int i = 0;
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
                        i++;
                        }
                        */
                        streamWriter.WriteLine("float marchingDist = sdSphere(pos,0.5);");
                        streamWriter.Flush();
                        Debug.Log("Clear");
                    }
                    else
                    {
                        Debug.Log(line);
                        streamWriter.WriteLine(line);
                        streamWriter.Flush();
                        i++;
                        Debug.Log(i);
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

