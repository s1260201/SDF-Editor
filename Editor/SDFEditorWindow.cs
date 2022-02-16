using UnityEngine;
using UnityEditor;

public class SDFEditorWindow : EditorWindow
{
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
    }
}