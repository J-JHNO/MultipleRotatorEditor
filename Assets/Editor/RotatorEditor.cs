using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.technical.test
{

    public class RotatorEditor : ScriptableWizard
    {
        // STEP 1
        public static int size = 1;
        public Rotator[] rotatorsToEdit = new Rotator[size];

        // STEP 2
        public string identifier = "";
        public float timeBeforeStoppingInSeconds = 0;
        public bool shouldReverseRotation = false;
        //public RotationSettings rotationsSettings = default;

        [MenuItem("Window/Custom/Rotators Mass Setter")]
        static void SelectAllOfRotatorWizard()
        {
            ScriptableWizard.DisplayWizard<RotatorEditor>("Rotators Mass Setter", "Validate changes");

            //ScriptableWizard.GetWindow();

            // Select all rotators in the scene
            /*
            Rotator[] rotators = (Rotator[])GameObject.FindObjectsOfType<Rotator>();
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (Rotator rotator in rotators)
            {
                gameObjects.Add(rotator.gameObject);
            }
            Selection.objects = gameObjects.ToArray();
            */
            
        }

        void OnWizardCreate()
        {

        }
    }
}
