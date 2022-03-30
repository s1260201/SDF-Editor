using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDFModeling;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using XNode;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        public List<SDFObj> list;
        [SerializeField] string readPath = "Assets/Shader/src/Sample.shader";
        [SerializeField] string writePath = "Assets/Shader/Export/Sample1.shader";
        [SerializeField] string ShaderName = "Sample";

        public void OutputShader()
        {
            SDFNode outputNode = sdfGraph.HeadNode();
            sdfGraph.current = outputNode;
            
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
                    if (reg.Match(line).Success)
                    {
                        while(true){
                            if (i > 10) break; // Safety
                            NextNode();
                            Debug.Log(i);
                            if (sdfGraph.current is Output) break;
                            if (sdfGraph.current is SphereNode)
                            {
                                SphereNode obj = (SphereNode)sdfGraph.current;
                                streamWriter.WriteLine("float dist" + i + " = sdSphere(float3(pos.x - "+ obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), " + obj.s + ");");
                            }
                            else
                            {
                                Debug.LogError("Selected an unknown Node.");
                                break;
                            }
                            streamWriter.Flush();
                            i++;
                        
                        }
                        streamWriter.Write("dist = ");
                        if(i > 0)
                        {
                            for (int j = 0; j < i-1; j++)
                            {
                                streamWriter.Write("min(");

                            }
                            streamWriter.Write("dist0");
                            for (int j = 1; j < i; j++)
                            {
                                streamWriter.Write(",dist" + j + ")");
                            }
                            streamWriter.WriteLine(";");
                        }
                        else
                        {
                            streamWriter.Write("dist0;");
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
        public void NextNode()
        {
            foreach(NodePort p in sdfGraph.current.Ports)
            {
                sdfGraph.current = p.Connection.node as SDFNode;
                break;
            }
        }
    }
}

