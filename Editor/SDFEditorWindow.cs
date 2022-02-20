using UnityEngine;
using UnityEditor;

using UnityEngine.UIElements;


public class SDFEditorWindow : EditorWindow
{
    public Material material;
    Editor gameObjectEditor;


    [MenuItem("Window/SDF-Editor")]
    static void Open()
    {
        /*
        var window = GetWindow<SDFEditorWindow>();
        //ウィンドウサイズ設定(minとmaxを=しているのはウィンドウサイズを固定するため)
        window.maxSize = window.minSize = new Vector2(WINDOWSIZE_W, WINDOWSIZE_H);
        */
        //EditorWindow.GetWindow("SDFEditorWindow");
        var window = GetWindow<SDFEditorWindow>("SDFEditorWindow");
    }

    void OnGUI()
    {
        //  実際のウィンドウのコードはここに書きます
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Material");
                material = (Material) EditorGUILayout.ObjectField(material, typeof(Material), true);
            EditorGUILayout.EndHorizontal();
        
            if (material != null)
            {
                if (gameObjectEditor == null)
                    gameObjectEditor = Editor.CreateEditor(material);

                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(100, 100), EditorStyles.whiteLabel);
            }
        EditorGUILayout.EndVertical();
        EditorGUILayout.LabelField("Material");
        EditorGUILayout.EndVertical();
    }
}