using System;
using UnityEditor;
using UnityEngine;

namespace UnityTemplateProjects.Editor
{
    [CustomEditor(typeof(FadingObstacle)), CanEditMultipleObjects]
    public class FadingObstacleEditor : UnityEditor.Editor
    {
        private FadingObstacle elevator;
        private void OnEnable()
        {
            elevator = (FadingObstacle) target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Reset Animation"))
            {
                elevator.ResetAnimation();
            }
            base.OnInspectorGUI();
        }
    }
}