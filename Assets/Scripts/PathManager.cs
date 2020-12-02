﻿using UnityEditor;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform[] points;

    public Vector3 GetPosition(int i)
    {
        return points[i].position;
    }

    private void OnDrawGizmos()
    {
        if (Selection.activeObject == null) return;
        if (!Selection.activeGameObject.transform.IsChildOf(transform)) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i+1].position);
            Gizmos.DrawIcon(points[i].position, "blendKey");
        }
    }
}