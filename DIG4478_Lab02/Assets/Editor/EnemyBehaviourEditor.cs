using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
#if UNITY_EDITOR

[CustomEditor(typeof(EnemyBehaviour)), CanEditMultipleObjects] 
public class EnemyBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {

       base.OnInspectorGUI();

        if(GUILayout.Button("Select all enemies"))
        {
            var allEnemyBehaviour = GameObject.FindObjectsOfType<EnemyBehaviour>();

            var allEnemyGameObjects = allEnemyBehaviour
                .Select(enemy => enemy.gameObject)
                .ToArray();
            Selection.objects = allEnemyGameObjects;
        }

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Clear selection"))
        {
            /*var cachedColor = GUI.backgroundColor;

            GUI.backgroundColor = Color.green;

            GUI.backgroundColor = cachedColor;*/
            Selection.objects = new Object[]
            {
                (target as EnemyBehaviour).gameObject
            };
        }
        EditorGUILayout.EndHorizontal();
        using (new EditorGUILayout.HorizontalScope())
        {
            /*bool isEnabled = true;*/
            var cachedColor = GUI.backgroundColor;
            /*GUI.backgroundColor = Color.red;*/
            var isEnabled = FindObjectsOfType<EnemyBehaviour>();

            if (isEnabled.Length > 0)
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.red;
            }   




            if (GUILayout.Button("Enable/Enable all enemy", GUILayout.Height(40)))
            {
                

                
                foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
                {
                    /*var Color = GUI.backgroundColor;

                    GUI.backgroundColor = Color.red;

                    GUI.backgroundColor = Color;*/
                    enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
                }
                /*if(isEnabled == false)
                {
                    GUI.backgroundColor = Color.red;
                }*/
            }

            

            /*if (isEnabled == true)
            {
                GUI.backgroundColor = Color.red;

            }*/
            //Draw the buttons



        }






    }
}
#endif
