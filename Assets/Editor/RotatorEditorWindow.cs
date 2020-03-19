using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace com.technical.test
{
    
    public class RotatorEditorWindow : ExtendedEditorWindow
    {
        Vector2 scrollPos;

        // PART 1
        public static int size = 0;
        public Rotator[] rotatorsToEdit = new Rotator[size];

        // PART 2
        bool groupEnabled;
        bool identifierBool = false;
        string identifier = "";
        bool timeBool = false;
        float timeBeforeStoppingInSeconds = 0;
        bool reverseBool = false;
        bool shouldReverseRotation = false;
        bool settingsBool = false;
        RotationSettings rotationsSettings = default;
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
            const int width = 500;
            const int height = 700;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);

            //window.serializedObject = new SerializedObject(this);
        }

        void OnGUI()
        {

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.BeginVertical();

            // PART 1
            //GUILayout.BeginArea(new Rect(0,0,500,100));
            GUILayout.Label("Rotators to edit", EditorStyles.boldLabel);

            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty rotatorsProperty = so.FindProperty("rotatorsToEdit");

            EditorGUILayout.PropertyField(rotatorsProperty, true); // True to show children
            
            //GUILayout.EndArea();

            GUILayout.Space(20f);

            // PART 2
            //GUILayout.BeginArea(new Rect(0, 150, 500, 300));
            GUILayout.Label("EDITOR", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            identifierBool = EditorGUILayout.BeginToggleGroup("Identifier :", identifierBool);
            identifier = EditorGUILayout.TextField(identifier);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            timeBool = EditorGUILayout.BeginToggleGroup("Time before stopping in seconds :", timeBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            reverseBool = EditorGUILayout.BeginToggleGroup("Sould reverse rotation :", reverseBool);
            shouldReverseRotation = EditorGUILayout.Toggle("", shouldReverseRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            settingsBool = EditorGUILayout.BeginToggleGroup("Rotation settings", settingsBool);
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            objectToRotateBool = EditorGUILayout.BeginToggleGroup("Object to rotate :", objectToRotateBool);
            objectToRotate = (Transform) EditorGUILayout.ObjectField("", objectToRotate, typeof(Transform), true);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            angleRotationBool = EditorGUILayout.BeginToggleGroup("Angle rotation :", angleRotationBool);
            angleRotation = EditorGUILayout.Vector3Field("", angleRotation);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            timeToRotateBool = EditorGUILayout.BeginToggleGroup("Time to rotate in seconds :", timeToRotateBool);
            timeBeforeStoppingInSeconds = EditorGUILayout.FloatField(timeBeforeStoppingInSeconds);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();

            GUILayout.Space(20f);

            EditorGUI.BeginDisabledGroup(rotatorsToEdit.Length == 0 || NonPersistentRotator());
            if (GUILayout.Button("Validate Changes")) {
                Validate();
            }
            EditorGUI.EndDisabledGroup();

            //GUILayout.EndArea();

            GUILayout.Space(20f);

            // PART 3
            //GUILayout.BeginArea(new Rect(0, 400, 500, 400));
            GUILayout.Label("Selected rotators", EditorStyles.boldLabel);

            GUILayout.Space(20f);

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

                for (int i=0; i<rotatorsToEdit.Length; i++)
                {
                    int x = -420, y = 400;
                    if (rotatorsToEdit[i] != null)
                    {
                        if (i%2 == 0)
                        {
                            //EditorGUILayout.BeginHorizontal();
                            x += 420;
                        }
                        //GUILayout.BeginArea(new Rect(x, y, 400, 200));
                        
                        GUIStyle s = (new GUIStyle(EditorStyles.textField));
                        s.normal.textColor = Color.blue;

                        EditorGUILayout.TextField(rotatorsToEdit[i].gameObject.name, s);
                        SerializedObject serializedObject = new SerializedObject(rotatorsToEdit[i]);

                        serializedObject.Update();

                        SerializedProperty identifierProperty = serializedObject.FindProperty("_identifier");
                        //Debug.Log(identifierProperty);
                        EditorGUILayout.PropertyField(identifierProperty, true);

                        SerializedProperty stopingTimeProperty = serializedObject.FindProperty("_timeBeforeStoppingInSeconds");
                        //Debug.Log(stopingTimeProperty);
                        EditorGUILayout.PropertyField(stopingTimeProperty, true);

                        SerializedProperty reverseProperty = serializedObject.FindProperty("_shouldReverseRotation");
                        //Debug.Log(reverseProperty);
                        EditorGUILayout.PropertyField(reverseProperty, true);

                        SerializedProperty settingsProperty = serializedObject.FindProperty("_rotationsSettings");
                        //Debug.Log(settingsProperty);
                        EditorGUILayout.PropertyField(settingsProperty, true);

                        //GUILayout.EndArea();
                        if (i % 2 != 0)
                        {
                            //EditorGUILayout.EndHorizontal();
                            x = 0;
                            y += 220;
                        }

                        serializedObject.ApplyModifiedProperties();
                        
                        GUILayout.Space(20f);
                        
                        
                    }
                }
            }
            
            so.ApplyModifiedProperties();

            //GUILayout.EndArea();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

        }

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

                            //Debug.Log(p.arrayElementType);

                            Debug.Log("p : " + p.ToString());
                            
                            rotator.transform.position = objectToRotate.position;
                            rotator.transform.rotation = objectToRotate.rotation;
                            rotator.transform.localScale = objectToRotate.localScale;
                            
                            
                            
                            
                            foreach (SerializedProperty prop in p)
                            {
                                if (prop.name == "position")
                                {
                                    prop.vector3Value = objectToRotate.position;
                                }
                                if (prop.name == "rotation")
                                {
                                    prop.quaternionValue = objectToRotate.rotation;
                                }
                                if (prop.name == "localScale")
                                {
                                    prop.vector3Value = objectToRotate.localScale;
                                }
                                
                            }
                            
                            
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

                    //serializedObject.UpdateIfRequiredOrScript();
                    //EditorGUILayout.PropertyField(serializedProperty);

                    //Debug.Log(serializedObject.hasModifiedProperties);
                    serializedObject.ApplyModifiedProperties();
               
            }
        }
        

    }
}
