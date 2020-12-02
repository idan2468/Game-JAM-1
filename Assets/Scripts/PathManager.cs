using System;
using UnityEditor;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector] public Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform p in transform)
        {
            points[i++] = p;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return points[i].position;
    }

    private void OnDrawGizmos()
    {
        if (Selection.activeObject == null) return;
        var selection = Selection.activeGameObject.transform;
        if (!selection.IsChildOf(transform) && !transform.IsChildOf(selection)) return;
        var points = transform.GetComponentsInChildren<Transform>();
        Gizmos.color = Color.red;
        for (int i = 1; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i+1].position);
            Gizmos.DrawIcon(points[i].position, "blendKey");
        }
    }
}
