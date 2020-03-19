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
            DrawDefaultInspector();
            Rotator rotator = (Rotator)target;
            if (GUILayout.Button("Rotators Mass Setter"))
            {
                //RotatorToEdit rotatorEditor = (RotatorToEdit)ScriptableObject.CreateInstance("RotatorToEdit");
                RotatorEditorWindow.Init();
            }
            
            
        }
    }
}
