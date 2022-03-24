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
            // ���̃C���X�y�N�^�[������\��
            base.OnInspectorGUI();

            // target��ϊ����đΏۂ��擾
            //MaterialSettings materialSettings = target as MaterialSettings;
            ExportShader exportShader = target as ExportShader;

            // public�ȃ��\�b�h�����s����{�^��
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

