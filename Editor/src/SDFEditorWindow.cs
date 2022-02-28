using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

/*
public class Obj : MonoBehaviour
{
    public LObject(int typeNum)
    {
        int objType = typeNum;
    }
    private List<LObject> _objList;
}
*/

public class SDFEditorWindow : EditorWindow
{
    public Material material;
    Editor gameObjectEditor;
    public Shader shader;
    private Shader temp_shader;
    private GameObject[] objlist;
    private ReorderableList reorderobjList;
    string sname;
    bool showingMenu = false;


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

        if (!showingMenu)
        {
            if (GUILayout.Button("New Shader"))
            {
                //shader = temp_shader;
                Debug.Log("Making");
                showingMenu = true;
            }
        }
        else
        {
            EditorGUILayout.LabelField("Editor Screen");
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