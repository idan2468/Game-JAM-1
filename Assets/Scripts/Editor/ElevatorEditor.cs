using System;
using UnityEditor;
using UnityEngine;

namespace UnityTemplateProjects.Editor
{
    [CustomEditor(typeof(ElevatorController)), CanEditMultipleObjects]
    public class ElevatorEditor : UnityEditor.Editor
    {
        private ElevatorController elevator;
        private void OnEnable()
        {
            elevator = (ElevatorController) target;
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