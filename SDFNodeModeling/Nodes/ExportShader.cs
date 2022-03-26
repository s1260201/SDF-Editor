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
            //Vector3 pos = new Vector3(0, 0, 0);
            //Sphere sphere = new Sphere();
            //sphere.s = 0.2f;
            //Box box = new Box(pos, new Vector3(0.2f, 0.2f, 0.2f));

            //list.Add(sphere);
            //list.Add(new Box(pos, new Vector3(0.2f, 0.2f, 0.2f)));
            //Debug.Log(list);
            Debug.Log(list[0].GetType());
            
            try
            {
                Regex reg = new Regex("// SDF");
                StreamReader streamReader = new StreamReader(readPath);
                StreamWriter streamWriter = new StreamWriter(writePath);
                string line = null;
                //string code;
                int i = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    //code = null;
                    //Debug.Log(line);
                    if (reg.Match(line).Success)
                    {
                        Debug.Log("Match");
                        Debug.Log(list);
                        while(i < list.Count){
                            Debug.Log(i);
                            if (list[i] is SDF.Sphere)
                            {
                                Sphere sphere = (Sphere)list[i];
                                streamWriter.WriteLine("float dist" + i + " sdSphere(pos, " + sphere.s + ");");
                            }
                            else if(list[i] is SDF.Box)
                            {
                                Box box = (Box)list[i];
                                streamWriter.WriteLine("float dist" + i + " sdBox(pos, float3(" + box.b.x + ", " + box.b.y + ", " + box.b.z + "));");
                                //streamWriter.WriteLine("float marchingDist = sdBox(pos,float3(0.5,0.5,0.5));");

                            }
                            else if(list[i] is SDF.RoundBox)
                            {
                                RoundBox roundBox = (RoundBox)list[i];
                                streamWriter.WriteLine("float dist" + i + " sdRoundBox(pos, float3(" + roundBox.b.x + ", " + roundBox.b.y + ", " + roundBox.b.z + "), " + roundBox.r + ");");
                                //streamWriter.WriteLine("float marchingDist = sdRoundBox(pos,float3(0.5,0.5,0.5),0.1);");
                            }
                            else
                            {
                                Debug.LogError("Selected an unknown Node.");
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

