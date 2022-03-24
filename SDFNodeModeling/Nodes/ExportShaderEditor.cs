using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SDF
{
    [CustomEditor(typeof(ExportShader))]
    public class ExportShaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // 元のインスペクター部分を表示
            base.OnInspectorGUI();

            // targetを変換して対象を取得
            //MaterialSettings materialSettings = target as MaterialSettings;
            ExportShader exportShader = target as ExportShader;

            // publicなメソッドを実行するボタン
            if (GUILayout.Button("Export Shader"))
            {
                exportShader.OutputShader();
            }

            /*
            if (GUILayout.Button("PrivateMethod!"))
            {
                exportShader.SendMessage("DataSet_Private", null, SendMessageOptions.DontRequireReceiver);
            }
            */
        }
    }
}

