using UnityEditor;
using UnityEngine;

namespace BallGatherer {
    [CustomEditor(typeof(RectangleBorder))]
    public class ERectangleBorder : Editor {
        public override void OnInspectorGUI() {
            var transformsAssigned = IsAllTransformsAssigned();
            
            var border = target as RectangleBorder;
            border.rightCubeTr = EditorGUILayout.ObjectField("Right Cube Transform", border.rightCubeTr, typeof(Transform), true) as Transform;
            border.bottomCubeTr = EditorGUILayout.ObjectField("Bottom Cube Transform", border.bottomCubeTr, typeof(Transform), true) as Transform;
            border.leftCubeTr = EditorGUILayout.ObjectField("Left Cube Transform", border.leftCubeTr, typeof(Transform), true) as Transform;
            border.topCubeTr = EditorGUILayout.ObjectField("Top Cube Transform", border.topCubeTr, typeof(Transform), true) as Transform;
            
            var requireSetup = false;
            if (!transformsAssigned && IsAllTransformsAssigned()) {
                transformsAssigned = true;
                requireSetup = true;
            }

            if (transformsAssigned) {
                var width = EditorGUILayout.FloatField("Width", border.width);
                if (!Mathf.Approximately(width, border.width)) {
                    requireSetup = true;
                    border.width = width;
                }
                var height = EditorGUILayout.FloatField("Height", border.height);
                if (!Mathf.Approximately(height, border.height)) {
                    requireSetup = true;
                    border.height = height;
                }
                var wallWidth = EditorGUILayout.FloatField("Wall Width", border.wallWidth);
                if (!Mathf.Approximately(wallWidth, border.wallWidth)) {
                    requireSetup = true;
                    border.wallWidth = wallWidth;
                }
                var wallHeight = EditorGUILayout.FloatField("Wall Height", border.wallHeight);
                if (!Mathf.Approximately(wallHeight, border.wallHeight)) {
                    requireSetup = true;
                    border.wallHeight = wallHeight;
                }
            }
            
            if (requireSetup || GUILayout.Button("Re-Setup")) {
                border.SetupBorder();
            }
        }

        private bool IsAllTransformsAssigned() {
            var border = target as RectangleBorder;
            return border.rightCubeTr != null && border.bottomCubeTr != null &&
                   border.leftCubeTr != null && border.topCubeTr != null;
        }
    }
}

