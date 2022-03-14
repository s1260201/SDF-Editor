using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class ExampleClass : EditorWindow
{
    GameObject gameObject;
    Shader shader;
    Editor gameObjectEditor;

    [MenuItem("Example/GameObject Editor")]
    static void ShowWindow()
    {
        GetWindowWithRect<ExampleClass>(new Rect(0, 0, 256, 256));
    }

    void OnGUI()
    {
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

        if (gameObject != null)
        {
            if (gameObjectEditor == null)
                gameObjectEditor = Editor.CreateEditor(gameObject);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
        }
    }
}