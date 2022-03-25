using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Reflection;

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
            

            OutputShader();  
        }

        public void OutputShader()
        {
            list = new List<SDFObj>();
            Vector3 pos = new Vector3(0, 0, 0);
            Sphere sphere = new Sphere();
            sphere.s = 0.2f;
            Box box = new Box(pos, new Vector3(0.2f, 0.2f, 0.2f));

            Debug.Log(sphere.GetType());

            list.Add(sphere);
            list.Add(new Box(pos, new Vector3(0.2f, 0.2f, 0.2f)));
            //Debug.Log(list);
            Debug.Log(list[0].GetType());
            
            try
            {
                Regex reg = new Regex("// SDF");
                StreamReader streamReader = new StreamReader(readPath);
                StreamWriter streamWriter = new StreamWriter(writePath);
                string line = null;
                int i = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    //Debug.Log(line);
                    if (reg.Match(line).Success)
                    {
                        Debug.Log("Match");
                        Debug.Log(list);
                        while(i < list.Count){
                            Debug.Log(i);
                            if (list[i] is SDF.Sphere)
                            {
                                Debug.Log("Get");
                                streamWriter.WriteLine("float marchingDist = sdSphere(pos,0.5);");
                            }
                            else if(list[i].GetType() == typeof(Box))
                            {
                                streamWriter.WriteLine("float marchingDist = sdBox(pos,float3(0.5,0.5,0.5));");

                            }
                            else if(list[i].GetType() == typeof(RoundBox))
                            {
                                streamWriter.WriteLine("float marchingDist = sdRoundBox(pos,float3(0.5,0.5,0.5),0.1);");

                            }
                            else
                            {
                                Debug.LogError("Unknown Node.");
                            }
                            streamWriter.Flush();
                            i++;
                        
                        }
                       
                        
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

