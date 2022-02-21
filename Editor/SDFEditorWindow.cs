using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


public class SDFEditorWindow : EditorWindow
{
    public Material material;
    Editor gameObjectEditor;
    public Shader shader;
    private Shader temp_shader;
    private GameObject[] objlist;
    private ReorderableList reorderobjList;


    [MenuItem("Window/SDF-Editor")]
    static void Open()
    {
        //EditorWindow.GetWindow("SDFEditorWindow");
        var window = GetWindow<SDFEditorWindow>("SDFEditorWindow");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Shader");
            shader = (Shader) EditorGUILayout.ObjectField(shader, typeof(Shader), true);
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Export")){
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