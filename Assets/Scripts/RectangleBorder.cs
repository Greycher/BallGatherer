using System;
using UnityEngine;

namespace BallGatherer {
    public class RectangleBorder : MonoBehaviour {
        public float width;
        public float height;
        public Transform rightCubeTr;
        public Transform bottomCubeTr;
        public Transform leftCubeTr;
        public Transform topCubeTr;
        public float wallHeight;
        public float wallWidth;

        public void SetupBorder() {
            SetupRightAndLeftBorder();
            SetupBottomAndTopBorder();
        }

        private void SetupRightAndLeftBorder() {
            var scale = new Vector3(wallWidth, wallHeight, height);
            var halfWidth = width * 0.5f;
            var halfWallHeight = wallHeight * 0.5f;
            
            rightCubeTr.localPosition = new Vector3(halfWidth + halfWallHeight, halfWallHeight, 0);
            rightCubeTr.localScale = scale;
            
            leftCubeTr.localPosition = new Vector3(-(halfWidth + halfWallHeight), halfWallHeight, 0);
            leftCubeTr.localScale = scale;
        }
        
        private void SetupBottomAndTopBorder() {
            var scale = new Vector3(width + 2 * wallWidth, wallHeight, wallWidth);
            var halfHeight = height * 0.5f;
            var halfWallHeight = wallHeight * 0.5f;
            var halfWallWidth = wallWidth * 0.5f;
            
            bottomCubeTr.localPosition = new Vector3(0, halfWallHeight, -(halfHeight + halfWallWidth));
            bottomCubeTr.localScale = scale;
            
            topCubeTr.localPosition = new Vector3(0, halfWallHeight, halfHeight + halfWallWidth);
            topCubeTr.localScale = scale;
        }

        public Vector3 GetMinPosition() {
            return transform.TransformPoint(new Vector3(-width * 0.5f, 0, -height * 0.5f));
        }
        
        public Vector3 GetMaxPosition() {
            return transform.TransformPoint(new Vector3(width * 0.5f, 0, height * 0.5f));
        }
    }
}

