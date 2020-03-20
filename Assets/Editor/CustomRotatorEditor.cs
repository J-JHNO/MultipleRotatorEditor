using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.technical.test
{
    [CustomEditor(typeof(Rotator))]
    public class CustomRotatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw default inspector and add a button
            DrawDefaultInspector(); 
            Rotator rotator = (Rotator)target;
            if (GUILayout.Button("Rotators Mass Setter"))
            {
                RotatorEditorWindow.Init(); // Pop the editor
            }
            
            
        }
    }
}
