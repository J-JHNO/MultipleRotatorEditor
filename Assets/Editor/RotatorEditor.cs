using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.technical.test
{

    public class RotatorEditor : ScriptableWizard
    {
        public static int size = 1;
        public Rotator[] rotatorsToEdit = new Rotator[size];


        [MenuItem("Window/Custom/Rotators Mass Setter")]
        static void SelectAllOfRotatorWizard()
        {
            ScriptableWizard.DisplayWizard<RotatorEditor>("Rotators Mass Setter", "Validate changes");

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
