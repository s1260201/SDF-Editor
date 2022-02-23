using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;


public class SDFEditorWindow : EditorWindow
{
    public Material material;
    Editor gameObjectEditor;
    public Shader shader;
    private Shader temp_shader;
    private GameObject[] objlist;
    private ReorderableList reorderobjList;
    string sname;


    [MenuItem("Window/SDF-Editor")]
    static void Open()
    {
        //EditorWindow.GetWindow("SDFEditorWindow");
        var window = GetWindow<SDFEditorWindow>("SDFEditorWindow");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        shader = (Shader) EditorGUILayout.ObjectField("Shader", shader, typeof(Shader), true);
        sname = EditorGUILayout.TextField("File name","");
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Export")){
            //shader = temp_shader;
            Debug.Log("Export");
        }
        /*
            if (material != null)
            {
                if (gameObjectEditor == null)
                    gameObjectEditor = Editor.CreateEditor(material);

                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(100, 100), EditorStyles.whiteLabel);
            }
        */
    }
}