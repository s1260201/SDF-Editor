using UnityEngine;
using UnityEditor;

public class SDFEditorWindow : EditorWindow
{
    private const float WINDOWSIZE_W = 500.0f;          //ウィンドウサイズ横幅
    private const float WINDOWSIZE_H = 200.0f;          //ウィンドウサイズ縦幅

    /// <summary>
    /// ウィンドウ表示
    /// </summary>
    [MenuItem("Window/SDF-Editor")]
    static void Open()
    {
        var window = GetWindow<SDFEditorWindow>();
        //ウィンドウサイズ設定(minとmaxを=しているのはウィンドウサイズを固定するため)
        window.maxSize = window.minSize = new Vector2(WINDOWSIZE_W, WINDOWSIZE_H);
    }
}