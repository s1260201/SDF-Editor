using UnityEngine;
using SDFModeling;
using System.IO;
using System.Text.RegularExpressions;
using System;
using XNode;
using SDF.Model;
using SDF.Controll;
using SDF.Controll.Bool;
using System.Collections.Generic;

namespace SDF
{
    public class ExportShader : MonoBehaviour
    {
        [SerializeField] SDFGraph sdfGraph;
        [SerializeField] string readPath = "Assets/Shader/src/Sample.shader";
        [SerializeField] string writePath = "Assets/Shader/Export/Sample1.shader";

        Queue<SDFNode> nodeQ = new Queue<SDFNode>();
        List<SDFNode> taskList = new List<SDFNode>();

        public void OutputShader()
        {
            //NextNode(sdfGraph.HeadNode());
            Stack<int> taskStack = new Stack<int>();

            NextNode(sdfGraph.HeadNode());

            Debug.Log("Start");

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
                        while (nodeQ.Count != 0)
                        {
                            if (i > 10)
                            {
                                Debug.Log("i is more than 10.");
                                break; // Safety
                            }
                            
                            SDFNode popNode = nodeQ.Dequeue();
                            Debug.Log(popNode.GetType());

                            if (popNode is UnionNode) Debug.Log("Union node");
                            else if (popNode is SphereNode) Debug.Log("Sphere node");
                            else if (popNode is BoxNode) Debug.Log("Box node");


                            Debug.Log(i);
                            if (popNode is SphereNode)
                            {
                                SphereNode obj = (SphereNode)popNode;
                                Debug.Log("Write a sphere code.");
                                streamWriter.WriteLine("float dist" + i + " = sdSphere(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), " + obj.s + ");");
                            }
                            else if (popNode is BoxNode)
                            {
                                BoxNode obj = (BoxNode)popNode;
                                Debug.Log("Write a Box code");
                                streamWriter.WriteLine("float dist" + i + " = sdBox(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), float3(" + obj.b.x + "," + obj.b.y + "," + obj.b.z + "));");
                            }
                            else if (popNode is RoundBoxNode)
                            {
                                RoundBoxNode obj = (RoundBoxNode)popNode;
                                Debug.Log("Write a RoundBox code");
                                streamWriter.WriteLine("float dist" + i + " = sdRoundBox(float3(pos.x - " + obj.p.x + ", pos.y -  " + obj.p.y + ", pos.z - " + obj.p.z + "), float3(" + obj.b.x + "," + obj.b.y + "," + obj.b.z + ")," + obj.r + ");");
                            }
                            else if (popNode is UnionNode)
                            {
                                UnionNode obj = (UnionNode)popNode;
                                Debug.Log("Write a Union code");
                                Queue<int> tmp = new Queue<int>();
                                int a = obj.listCounter();
                                // taskStackì‡ÇÃíºãﬂaå¬ÇÃÉmÅ[ÉhÇëŒè€

                                streamWriter.Write("float dist" + i + " = ");

                                for (int j = 0; j < a - 1; j++)
                                {
                                    streamWriter.Write("min(");
                                }
                                streamWriter.Write("dist" + taskStack.Pop());
                                for (int j = 1; j < a; j++)
                                {
                                    streamWriter.Write(",dist" + taskStack.Pop() + ")");
                                }
                                streamWriter.WriteLine(";");
                            }
                            else if (popNode is Head)
                            {
                                Debug.Log("Write a Head Code");
                                streamWriter.Write("dist = dist" + --i + ";");
                            }
                            else if (popNode is DifferenceNode)
                            {
                                // Diff is max(-A, B)
                                DifferenceNode obj = (DifferenceNode)popNode;
                                Debug.Log("Write a DifferenceNode");
                                int a = 0;
                                foreach (NodePort p in obj.Ports)
                                {
                                    if (p.fieldName != "beforeNode")
                                        Debug.Log("Diff : " + a);
                                    a++;
                                }
                            }
                            else
                            {
                                Debug.LogError("Selected an unknown Node.");
                                break;
                            }

                            streamWriter.Flush();
                            Debug.Log("Flush!");
                            taskStack.Push(i); // ControllNodeà»â∫Ç…Ç†ÇÈObjNodeÇÃî‘çÜiÇì¸ÇÍÇƒÇ®Ç≠ÅB
                            i++;
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
        
        public void NextNode(SDFNode node)
        {
            //Debug.Log("");
            if(node is DifferenceNode)
            {
                foreach (NodePort p in node.Ports)
                {
                    if (p.fieldName == "nodes")
                    {
                        try
                        {
                            NextNode(p.Connection.node as SDFNode);
                            break;
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError("Null conect");
                        }
                    }
                }
                foreach (NodePort p in node.Ports)
                {
                    if (p.fieldName == "targetNodes")
                    {
                        try
                        {
                            NextNode(p.Connection.node as SDFNode);
                            break;
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError("Null conect");
                        }
                    }
                }
            }
            else
            {
                foreach (NodePort p in node.Ports)
                {
                    if (p.fieldName != "beforeNode")
                    {
                        try
                        {
                            NextNode(p.Connection.node as SDFNode);
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError("Null conect");
                        }
                    }
                }
            }           
            if (node is UnionNode) Debug.Log("Union node");
            else if (node is SphereNode) Debug.Log("Sphere node");
            else if (node is BoxNode) Debug.Log("Box node");
            nodeQ.Enqueue(node);
            
        }
        
    }
}

