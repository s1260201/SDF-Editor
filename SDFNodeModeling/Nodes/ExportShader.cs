using UnityEngine;
using SDFModeling;
using System.IO;
using System.Text.RegularExpressions;
using System;
using XNode;
using SDF.Model;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        [SerializeField] string readPath = "Assets/Shader/src/Sample.shader";
        [SerializeField] string writePath = "Assets/Shader/Export/Sample1.shader";
        //[SerializeField] string ShaderName = "Sample";

        public void OutputShader()
        {
            SDFNode outputNode = sdfGraph.OutputNode();
            sdfGraph.current = outputNode;

            try
            {
                Regex reg = new Regex("// SDF");
                StreamReader streamReader = new StreamReader(readPath);
                StreamWriter streamWriter = new StreamWriter(writePath);
                string line = null;
                int i = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (reg.Match(line).Success)
                    {
                        while (true)
                        {
                            if (i > 5)
                            {
                                Debug.Log("i is more than 5.");
                                break; // Safety
                            }
                            Debug.Log("GotoNN");
                            NextNode();
                            Debug.Log(i);
                            if (sdfGraph.current is Output) break;
                            if (sdfGraph.current is SphereNode)
                            {
                                SphereNode obj = (SphereNode)sdfGraph.current;
                                Debug.Log("Write a sphere code.");
                                streamWriter.WriteLine("float dist" + i + " = sdSphere(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), " + obj.s + ");");
                            }
                            else if (sdfGraph.current is BoxNode)
                            {
                                BoxNode obj = (BoxNode)sdfGraph.current;
                                Debug.Log("Write a Box code");
                                streamWriter.WriteLine("float dist" + i + " = sdBox(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), float3(" + obj.b.x + "," + obj.b.y + "," + obj.b.z + "));");
                            }
                            else if (sdfGraph.current is RoundBoxNode)
                            {
                                RoundBoxNode obj = (RoundBoxNode)sdfGraph.current;
                                Debug.Log("Write a RoundBox code");
                                streamWriter.WriteLine("float dist" + i + " = sdRoundBox(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), float3(" + obj.b.x + "," + obj.b.y + "," + obj.b.z + ")," + obj.r + ");");
                            }
                            else
                            {
                                Debug.LogError("Selected an unknown Node.");
                                break;
                            }
                            streamWriter.Flush();
                            Debug.Log("Flush!");
                            i++;
                            Debug.Log("i is added 1");
                        }
                        streamWriter.Write("dist = ");
                        if (i > 0)
                        {
                            for (int j = 0; j < i - 1; j++)
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
            foreach (NodePort p in sdfGraph.current.Ports)
            {
                Debug.Log("foreach");
                if (p.fieldName == "nextNode")
                {
                    sdfGraph.current = p.Connection.node as SDFNode;
                    break;
                }

            }
        }
        public void BackNodes()
        {
            foreach (NodePort p in sdfGraph.current.Ports)
            {
                Debug.Log("foreach");
                if (p.fieldName == "nextNode")
                {
                    sdfGraph.current = p.Connection.node as SDFNode;
                    break;
                }
            }
        }
    }
}

