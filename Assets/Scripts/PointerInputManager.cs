using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BallGatherer {
    [RequireComponent(typeof(Canvas))]
    public class PointerInputManager : InputManager, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        public RectTransform joyPad;
        public Vector2 horizontalDragBoundsInInches = new Vector2(0.05f, 0.2f);
        public Vector2 verticalDragBoundsInInches = new Vector2(0.05f, 0.2f);
        
        public Animator joyPadAnimator;
        public string horizontalParamName;
        public string verticalParamName;
        public float animSpeed = 12;
        
        private const int NonePointerID = Int32.MinValue;

        private RectTransform _canvasTr;
        private Vector2 _dpi;
        private Vector2 _dragDirection;
        private int _pointerID = NonePointerID;

        private void Awake() {
            _dpi = GetDPI();
            _canvasTr = transform as RectTransform;
            DisableJoyPad();
        }

        private Vector2 GetDPI() {
            Vector2 dpi;
            //Unity documents notes that dpi value could be wrong in android devices
            //See the scripting document of Screen.dpi field for the details
            if (!Application.isEditor && Application.platform == RuntimePlatform.Android) {
                AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
 
                AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
                activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

                dpi = new Vector2(metrics.Get<float>("xdpi"), metrics.Get<float>("ydpi"));
            }
            else {
                dpi = new Vector2(Screen.dpi, Screen.dpi);
            }

            return dpi;
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (_pointerID == NonePointerID) {
                _pointerID = eventData.pointerId;
                EnableJoyPadAt(eventData.position);
            }
        }

        private void EnableJoyPadAt(Vector2 position) {
            joyPad.anchoredPosition = ToLocalScreenPosition(position);
            joyPad.gameObject.SetActive(true);
        }
        
        private Vector2 ToLocalScreenPosition(Vector2 position) {
            var currResolution = new Vector2(Screen.width, Screen.height);
            var rect = _canvasTr.rect;
            var referenceResolution = new Vector2(rect.width, rect.height);
            var inverseResolutionScale = referenceResolution / currResolution;
            return position * inverseResolutionScale;
        }

        void IDragHandler.OnDrag(PointerEventData eventData) {
            if(eventData.pointerId == _pointerID) {
                Drag(eventData.pressPosition, eventData.position);
            }
        }
    
        public void OnPointerUp(PointerEventData eventData) {
            if (eventData.pointerId == _pointerID) {
                _pointerID = NonePointerID;
                _dragDirection = Vector2.zero;
                DisableJoyPad();
            }
        }

        private void DisableJoyPad() {
            joyPad.gameObject.SetActive(false);
            joyPadAnimator.SetFloat(horizontalParamName, 0);
            joyPadAnimator.SetFloat(verticalParamName, 0);
        }

        private void Drag(Vector2 pressPosition, Vector2 pointerPosition) {
            var deltaPosition = pointerPosition - pressPosition;
            var deltaInch = deltaPosition / _dpi;
            _dragDirection = new Vector2(
                Normalize(Mathf.Abs(deltaInch.x), horizontalDragBoundsInInches.x, horizontalDragBoundsInInches.y) * Mathf.Sign(deltaInch.x),
                Normalize(Mathf.Abs(deltaInch.y), verticalDragBoundsInInches.x, verticalDragBoundsInInches.y) * Mathf.Sign(deltaInch.y));
        }
        
        private float Normalize(float value, float minValue, float maxValue, float minNormalizedValue = 0, float maxNormalizedValue = 1)
        {
            return Mathf.Clamp(NormalizeUnclamped(value, minValue, maxValue, minNormalizedValue, maxNormalizedValue), minNormalizedValue, maxNormalizedValue);
        }
        
        private float NormalizeUnclamped(float value, float minValue, float maxValue, float minNormalizedValue = 0, float maxNormalizedValue = 1)
        {
            return (value - minValue) / (maxValue - minValue) * (maxNormalizedValue - minNormalizedValue) + minNormalizedValue;
        }
        
        private void Update() {
            if (_pointerID != NonePointerID) {
                UpdateJoyPadAnimation();
                if (_controller != null) {
                    _controller.Drag(_dragDirection);
                }
            }
        }

        private void UpdateJoyPadAnimation() {
            joyPadAnimator.SetFloat(horizontalParamName,
                Mathf.MoveTowards(joyPadAnimator.GetFloat(horizontalParamName), _dragDirection.x,
                    animSpeed * Time.deltaTime));
            joyPadAnimator.SetFloat(verticalParamName,
                Mathf.MoveTowards(joyPadAnimator.GetFloat(verticalParamName), _dragDirection.y,
                    animSpeed * Time.deltaTime));
        }
    }
}
