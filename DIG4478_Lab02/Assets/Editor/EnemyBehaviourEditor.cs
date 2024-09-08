using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;
using JetBrains.Annotations;
#if UNITY_EDITOR

[CustomEditor(typeof(EnemyBehaviour)), CanEditMultipleObjects] 
public class EnemyBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {

        //Lets you edit the Inspector GUI
       base.OnInspectorGUI();

        //Begins the horizontal layout of the butons
       EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all enemies"))
        {
            //Finds any game objects that have the EnemyBehaviour script
            var allEnemyBehaviour = GameObject.FindObjectsOfType<EnemyBehaviour>();

            var allEnemyGameObjects = allEnemyBehaviour
                .Select(enemy => enemy.gameObject)
                .ToArray();
            Selection.objects = allEnemyGameObjects;
        }

        //Clear selection button
        if(GUILayout.Button("Clear selection"))
        {
            
            Selection.objects = new Object[]
            {
                (target as EnemyBehaviour).gameObject
            };
        }


        EditorGUILayout.EndHorizontal();
        using (new EditorGUILayout.HorizontalScope())
        {
            //Sets the color to the original color in case its needed 
            var cachedColor = GUI.backgroundColor;

            //Checks if there is any objects with the EnemyBehaviour script
            var objects = FindObjectsOfType<EnemyBehaviour>();

            if (objects.Length > 0)
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.red;
            }


            if (GUILayout.Button("Enable/Disable all enemy", GUILayout.Height(40)))
            {
                

                //For each enemy that has an EnemyBehaviour script, set them to the opposite condition (If they are enabled or not)
                foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
                {
                    enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
                }
            }
          



        }

        //Makes a serializedObject for all conditions
        serializedObject.Update();

        var health = serializedObject.FindProperty("health");
        var attackPt = serializedObject.FindProperty("attackPt");
        var size = serializedObject.FindProperty("size");

        serializedObject.ApplyModifiedProperties();
        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            var temp = EditorGUILayout.Slider("Health", health.floatValue, 0, 10);


            if (changeScope.changed)
            {
                health.floatValue = temp;
            }
            if (health.floatValue < 0)
            {
                EditorGUILayout.HelpBox("The health cannot be less than 0!", MessageType.Warning);
            }

            if (size.floatValue > 3)
            {
                EditorGUILayout.HelpBox("The object cannot be above 3f!", MessageType.Warning);
            }
            else if (size.floatValue < 0)
            {
                EditorGUILayout.HelpBox("The object cannot be below 0!", MessageType.Warning);
            }


        }








    }
}
#endif
