using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace com.technical.test
{
    public class RotatorToEdit : EditorWindow
    {
        // PART 1
        public static int size = 1;
        public Rotator[] rotatorsToEdit = new Rotator[size];

        // PART 2
        bool groupEnabled;
        bool identifierBool = true;
        string identifier = "";
        bool timeBool = true;
        float timeBeforeStoppingInSeconds = 0;
        bool reverseBool = true;
        bool shouldReverseRotation = false;
        bool settingsBool = true;
        RotationSettings rotationsSettings = default;
        bool objectToRotateBool = true;
        string objectToRotate = "";
        bool angleRotationBool = true;
        Vector3 angleRotation;
        bool timeToRotateBool = true;
        float timeToRotateInSeconds;

        [MenuItem("Window/Custom/Rotators Mass Setter")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            RotatorToEdit window = (RotatorToEdit)EditorWindow.GetWindow(typeof(RotatorToEdit));
            const int width = 500;
            const int height = 800;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);

            window.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            // PART 1
            GUILayout.Label("Rotators to edit", EditorStyles.boldLabel);

            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty rotatorsProperty = so.FindProperty("rotatorsToEdit");

            EditorGUILayout.PropertyField(rotatorsProperty, true); // True to show children

            GUILayout.Space(20f);

            // PART 2
            GUILayout.Label("EDITOR", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            identifierBool = EditorGUILayout.BeginToggleGroup("Identifier :", identifierBool);
            identifier = EditorGUILayout.TextField(identifier);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            timeBool = EditorGUILayout.BeginToggleGroup("Time before stopping in seconds :", timeBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            reverseBool = EditorGUILayout.BeginToggleGroup("Sould reverse rotation :", reverseBool);
            shouldReverseRotation = EditorGUILayout.Toggle("", shouldReverseRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();
            
            settingsBool = EditorGUILayout.BeginToggleGroup("Rotation settings", settingsBool);
            EditorGUILayout.BeginHorizontal();
            objectToRotateBool = EditorGUILayout.BeginToggleGroup("Object to rotate :", objectToRotateBool);
            objectToRotate = EditorGUILayout.TextField("", objectToRotate);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            angleRotationBool = EditorGUILayout.BeginToggleGroup("Angle rotation :", angleRotationBool);
            angleRotation = EditorGUILayout.Vector3Field("", angleRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            timeToRotateBool = EditorGUILayout.BeginToggleGroup("Time to rotate in seconds :", timeToRotateBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndToggleGroup();

            //GUI.Box(new Rect(0, 0, 400, 400), "test");

            GUILayout.Space(20f);

            // PART 3
            GUILayout.Label("Selected rotators", EditorStyles.boldLabel);

            foreach (Rotator rotator in rotatorsToEdit)
            {
                if (rotator != null)
                {
                    SerializedObject serializedObject = new UnityEditor.SerializedObject(rotator);

                    serializedObject.Update();

                    SerializedProperty serializedProperty = serializedObject.FindProperty("Rotator");

                    EditorGUILayout.PropertyField(serializedProperty);

                    serializedObject.ApplyModifiedProperties();
                    

                    Component r = rotator.GetComponent<Rotator>();


                    EditorGUILayout.ObjectField(rotator, typeof(Object), true);
                }
            }

            so.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();

        }

        
    }
}
