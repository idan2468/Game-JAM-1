using UnityEditor;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform[] points;

    public Vector3 GetPosition(int i)
    {
        return points[i].position;
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i+1].position);
            Gizmos.DrawIcon(points[i].position, "blendKey");
        }
    }
}
