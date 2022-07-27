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

        public void OutputShader()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
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
                bool transforemReset = false;

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

                            Debug.Log(i);

                            if (popNode is SDFObjNode)
                            {
                                SDFObjNode obj = (SDFObjNode)popNode;
                                streamWriter.WriteLine(obj.Calcsd(i));
                            }
                            else if (popNode is TransformNode)
                            {
                                TransformNode obj = (TransformNode)popNode;
                                if (transforemReset)
                                {
                                    int a = i - 1;
                                    streamWriter.WriteLine("dist" + a + " /= " + Mathf.Max(1 / obj.nodeScale.x, 1 / obj.nodeScale.y, 1 / obj.nodeScale.z) + ";");
                                    streamWriter.WriteLine("pos = original_pos;");
                                    transforemReset = false;
                                }
                                else
                                {
                                    streamWriter.Write(obj.CalcOpe());
                                    transforemReset = true;
                                }
                            }
                            else if (popNode is UnionNode)
                            {
                                UnionNode obj = (UnionNode)popNode;
                                Debug.Log("Write a Union code");
                                int a = obj.listCounter();

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
                            else if (popNode is SDFOperate)
                            {
                                streamWriter.WriteLine(popNode.CalcOpe());
                            }
                            else if (popNode is SDFBoolNode)
                            {
                                SDFBoolNode obj = (SDFBoolNode)popNode;
                                streamWriter.WriteLine(obj.CalcBool(taskStack.Pop(), taskStack.Pop(), i));
                            }
                            else if (popNode is Head)
                            {
                                Debug.Log("Write a Head Code");
                                streamWriter.WriteLine("dist = dist" + --i + ";");
                            }
                            else if (popNode is SmoothUnionNode)
                            {
                                SmoothUnionNode obj = (SmoothUnionNode)popNode;
                                Debug.Log("Write a SmoothUnionNode");
                                streamWriter.WriteLine("float dist" + i + " = smin(dist" + taskStack.Pop() + ", dist" + taskStack.Pop() + ");");
                            }
                            else if (popNode is RepeatNode)
                            {
                                RepeatNode obj = (RepeatNode)popNode;
                                Debug.Log("Write a RepeatNode");
                                String repPlane = obj.RepPlane();
                                //streamWriter.WriteLine("float3 rp = pos;");
                                streamWriter.WriteLine("pos." + repPlane + " = repeat(pos." + repPlane + ", " + obj.interval + ");");
                            }
                            else
                            {
                                Debug.LogError("Selected an unknown Node.");
                                break;
                            }

                            streamWriter.Flush();
                            Debug.Log("Flush!");
                            if (popNode is not TransformNode)
                            {
                                if (popNode is RepeatNode) Debug.Log("repnode");
                                else
                                {
                                    taskStack.Push(i); // ControllNodeˆÈ‰º‚É‚ ‚éObjNode‚Ì”Ô†i‚ð“ü‚ê‚Ä‚¨‚­B
                                    i++;
                                }
                                
                            }
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
            sw.Stop();
            Debug.Log(sw.Elapsed);
        }

        public void NextNode(SDFNode node)
        {
            //Debug.Log("");
            if (node is DifferenceNode)
            {
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
                foreach (NodePort p in node.Ports)
                {
                    if (p.fieldName == "negativeNodes")
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
            else if (node is RepeatNode || node is TransformNode)
            {
                nodeQ.Enqueue(node);
                foreach (NodePort p in node.Ports)
                {
                    if (p.fieldName == "node")
                    {
                        try
                        {
                            NextNode(p.Connection.node as SDFNode);
                            if (node is TransformNode)
                                nodeQ.Enqueue(node);
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError("Null conect");
                        }
                    }
                }
                return;
            }
            else if (node is SDFObjNode)
            {

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
            nodeQ.Enqueue(node);

        }

    }
}

