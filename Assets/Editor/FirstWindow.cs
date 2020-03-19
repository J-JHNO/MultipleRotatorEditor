using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.technical.test
{
    public class FirstWindow : EditorWindow
    {
        [MenuItem("Window/Custom/Rotators Mass Setter")]
        public static void Init()
        {
            /*
            // Get existing open window or if none, make a new one:
            RotatorEditorWindow window = GetWindow<RotatorEditorWindow>("Rotators Mass Setter");
            const int width = 500;
            const int height = 200;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);

            //window.serializedObject = new SerializedObject(this);
            */
        }

        public void OnGUI()
        {
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty rotatorsProperty = so.FindProperty("rotatorsToEdit");

            //GUILayout.BeginArea(new Rect(0,0,500,100));
            GUI.backgroundColor = Color.white;

            GUILayout.Label("ROTATORS TO EDIT", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(rotatorsProperty, true); // True to show children

            //GUILayout.EndArea();

            GUILayout.Space(20f);
        }
    }
}
