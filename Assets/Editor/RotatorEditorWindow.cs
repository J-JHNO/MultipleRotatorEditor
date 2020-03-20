using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace com.technical.test
{
    
    public class RotatorEditorWindow : ExtendedEditorWindow
    {
        Vector2 scrollPos;

        // PART 1 (Selection of the rotators we want to edit)
        public static int size = 0;
        public Rotator[] rotatorsToEdit = new Rotator[size];

        // PART 2 (Selection of the variables we want to changes for the selected rotators)
        bool groupEnabled;
        bool identifierBool = false;
        string identifier = ""; 
        bool timeBool = false;
        float timeBeforeStoppingInSeconds = 0; 
        bool reverseBool = false;
        bool shouldReverseRotation = false; 
        bool settingsBool = false;

        // Rotation settings
        bool objectToRotateBool = false;
        Transform objectToRotate; 
        bool angleRotationBool = false;
        Vector3 angleRotation;
        bool timeToRotateBool = false;
        float timeToRotateInSeconds;

        [MenuItem("Window/Custom/Rotators Mass Setter")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            RotatorEditorWindow window = GetWindow<RotatorEditorWindow>("Rotators Mass Setter");

            // Changing position and resolution of the window
            const int width = 500;
            const int height = 700;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);
        }

        void OnGUI()
        {
            // Beginning a scrollView in case the window's components leave the window's view
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.BeginVertical();

            // PART 1 (Selection of the rotators we want to edit)
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty rotatorsProperty = so.FindProperty("rotatorsToEdit");
            WindowRotatorsToEdit(rotatorsProperty);
            
            // PART 2 (Selection of the variables we want to changes for the selected rotators)
            WindowEditor();
            
            // PART 3 (Display of the selected rotators)
            WindowSelectedRotators();
            
            so.ApplyModifiedProperties();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        // Selection of the rotators we want to change
        private void WindowRotatorsToEdit(SerializedProperty prop)
        {
            GUI.backgroundColor = Color.white;

            GUILayout.Label("ROTATORS TO EDIT", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(prop, true); // True to show children

            GUILayout.Space(20f);

            HorizontalLine(Color.blue);
        }

        // Selection of the variables in each selected rotator we want to change
        private void WindowEditor()
        {
            GUILayout.Label("EDITOR", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            // Refer to _identifier attribute we want to change
            EditorGUILayout.BeginHorizontal();
            identifierBool = EditorGUILayout.BeginToggleGroup("Identifier :", identifierBool);
            identifier = EditorGUILayout.TextField(identifier);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Refer to _timeBeforeStoppingInSeconds attribute we want to change
            EditorGUILayout.BeginHorizontal();
            timeBool = EditorGUILayout.BeginToggleGroup("Time before stopping in seconds :", timeBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Refer to _shouldReverseRotation attribute we want to change
            EditorGUILayout.BeginHorizontal();
            reverseBool = EditorGUILayout.BeginToggleGroup("Sould reverse rotation :", reverseBool);
            shouldReverseRotation = EditorGUILayout.Toggle("", shouldReverseRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Refer to _rotationsSettings attribute we want to change
            settingsBool = EditorGUILayout.BeginToggleGroup("Rotation settings", settingsBool);
            EditorGUI.indentLevel++;
            //      Refer to ObjectToRotate attribute
            EditorGUILayout.BeginHorizontal();
            objectToRotateBool = EditorGUILayout.BeginToggleGroup("Object to rotate :", objectToRotateBool);
            objectToRotate = (Transform)EditorGUILayout.ObjectField("", objectToRotate, typeof(Transform), true);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            //      Refer to AngleRotation attribute
            EditorGUILayout.BeginHorizontal();
            angleRotationBool = EditorGUILayout.BeginToggleGroup("Angle rotation :", angleRotationBool);
            angleRotation = EditorGUILayout.Vector3Field("", angleRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            //      Refer to TimeToRotateInSeconds attribute
            EditorGUILayout.BeginHorizontal();
            timeToRotateBool = EditorGUILayout.BeginToggleGroup("Time to rotate in seconds :", timeToRotateBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();

            GUILayout.Space(20f);

            /* 
               User must have a rotators to edit list with at least one element 
               && 
               all elements of the list must have been selected 

               If one of these conditions is false, the validate button is disable
            */
            EditorGUI.BeginDisabledGroup(rotatorsToEdit.Length == 0 || NonPersistentRotator());
            var style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.blue;
            style.alignment = TextAnchor.MiddleCenter;
            style.border = GUI.skin.button.border;
            style.margin = new RectOffset(150, 150, 5, 5);
            if (GUILayout.Button("Validate Changes", style))
            {
                Validate();
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.Space(20f);

            HorizontalLine(Color.blue);
        }

        // Display of the selected rotators
        private void WindowSelectedRotators()
        {
            GUILayout.Label("SELECTED ROTATORS", EditorStyles.boldLabel);

            GUILayout.Space(20f);

            // Warning message to help understand why we can't validate
            if (rotatorsToEdit.Length == 0)
            {
                EditorGUILayout.HelpBox("There is no rotator to modify !", MessageType.Warning);
            }
            else if (NonPersistentRotator())
            {
                EditorGUILayout.HelpBox("A rotator is missing !", MessageType.Warning);
            }
            else
            {

                Rotator[] rotators = (Rotator[])GameObject.FindObjectsOfType<Rotator>();
                List<GameObject> gameObjects = new List<GameObject>();

                

                for (int i = 0; i < rotatorsToEdit.Length; i++)
                {
                    if (rotatorsToEdit[i] != null)
                    {
                        
                        GUIStyle s = (new GUIStyle(EditorStyles.textField));
                        s.normal.textColor = Color.blue;

                        EditorGUILayout.TextField(rotatorsToEdit[i].gameObject.name, s);
                        SerializedObject serializedObject = new SerializedObject(rotatorsToEdit[i]);

                        serializedObject.Update();

                        SerializedProperty identifierProperty = serializedObject.FindProperty("_identifier");
                        EditorGUILayout.PropertyField(identifierProperty, true);

                        SerializedProperty stopingTimeProperty = serializedObject.FindProperty("_timeBeforeStoppingInSeconds");
                        EditorGUILayout.PropertyField(stopingTimeProperty, true);

                        SerializedProperty reverseProperty = serializedObject.FindProperty("_shouldReverseRotation");
                        EditorGUILayout.PropertyField(reverseProperty, true);

                        SerializedProperty settingsProperty = serializedObject.FindProperty("_rotationsSettings");
                        EditorGUILayout.PropertyField(settingsProperty, true);
                        
                        serializedObject.ApplyModifiedProperties();

                        GUILayout.Space(20f);
                    }
                }
            }
        }

        // Create a horizontal line with the specified color 
        static void HorizontalLine(Color color)
        {
            GUIStyle horizontalLine;
            horizontalLine = new GUIStyle();
            horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            horizontalLine.margin = new RectOffset(0, 0, 4, 4);
            horizontalLine.fixedHeight = 2;

            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, horizontalLine);
            GUI.color = c;
        }
        
        // Make sure all elements in the list of rotators we want to select are filed
        bool NonPersistentRotator()
        {
            bool nonPersistent = false;
            foreach (Rotator rotator in rotatorsToEdit)
            {
                if (rotator == null)
                {
                    nonPersistent = true;
                }
            }
            return nonPersistent;
        }

        // Method to apply the changes
        void Validate ()
        {
            foreach (Rotator rotator in rotatorsToEdit)
            {
                Rotator component = rotator.GetComponent<Rotator>();
                SerializedObject serializedObject = new UnityEditor.SerializedObject(rotator);
                if (identifierBool)
                {
                    SerializedProperty serializedProperty = serializedObject.FindProperty("_identifier");
                    serializedProperty.stringValue = identifier;
                }
                if (timeBool)
                {
                    SerializedProperty serializedProperty2 = serializedObject.FindProperty("_timeBeforeStoppingInSeconds");
                    serializedProperty2.floatValue = timeBeforeStoppingInSeconds;
                }
                if (reverseBool)
                {
                    SerializedProperty serializedProperty3 = serializedObject.FindProperty("_shouldReverseRotation");
                    serializedProperty3.boolValue = shouldReverseRotation;
                }
                if (settingsBool)
                {
                    SerializedProperty serializedProperty4 = serializedObject.FindProperty("_rotationsSettings");

                    
                    foreach (SerializedProperty p in serializedProperty4)
                    {
                        if (objectToRotateBool && p.name == "ObjectToRotate")
                        {
                            p.objectReferenceValue = objectToRotate;
                        }
                        if (angleRotationBool && p.name == "AngleRotation")
                        {
                            p.vector3Value = angleRotation;
                        }
                        if (timeToRotateBool && p.name == "TimeToRotateInSeconds")
                        {
                            p.floatValue = timeBeforeStoppingInSeconds;
                        }
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
