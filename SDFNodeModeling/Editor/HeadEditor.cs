using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using SDF.Controll;

[CustomNodeEditor(typeof(Head))]
public class HeadEditor : NodeEditor
{
    private Head head;
    public override void OnBodyGUI()
    {
        if (head == null) head = target as Head;
        serializedObject.Update();
        /*
        if (GUILayout.Button("Export"))
        {

        }
        */
        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("noise"));
        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("shadow"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("nextNode"));
    }
}
