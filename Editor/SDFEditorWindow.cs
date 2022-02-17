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
        GetWindow<SDFEditorWindow>("SDFEditorWindow");
    }

    void OnGUI()
    {
        //  実際のウィンドウのコードはここに書きます
        var label = new Label("Material");
        material = (Material) EditorGUILayout.ObjectField(material, typeof(Material), true);

        if (material != null)
        {
            if (gameObjectEditor == null)
                gameObjectEditor = Editor.CreateEditor(material);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel);
        }
    }
}