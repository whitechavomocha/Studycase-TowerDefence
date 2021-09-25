using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.black;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 currentWaypointPoint = Waypoint.CurrentPos + Waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 
                Quaternion.identity, 0.2f, new Vector3(0.1f, 0.1f, 0.1f), Handles.SphereHandleCap);
            
            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPos;
            }
        }
    }
}
