using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BallGatherer {
    public class PointerInputManager : InputManager, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        private const string key = nameof(PointerInputManager);
        private const int NonePointerID = Int32.MinValue;
        
        public static PointerInputManager GetForLevel(Level level) {
            level.TryGetLevelObjectFromDictionary(key, out PointerInputManager inputManager);
            return inputManager;
        }

        public Canvas canvas;
        public CanvasGroup canvasGroup;
        public RectTransform joyPad;
        public float minDragInInches = 0.1f;
        public float maxDragInInches = 0.3f;
        
        [Header("Animations")]
        public Animator joyPadAnimator;
        public string horizontalParamName;
        public string verticalParamName;
        public float animSpeed = 12;


        public bool Enable{
            set {
                canvasGroup.interactable = value;
                if (!value) {
                    ReleaseInput();
                }
            }
        }

        private float _dpi;
        private Vector2 _drag;
        private int _pointerID = NonePointerID;
        
        public override void Initialize(Level level) {
            level.AddLevelObjectToDictionary(key, this);
            _dpi = GetDPI();
        }
        
        private float GetDPI() {
            //Unity documents notes that dpi value could be wrong in android devices
            //See the scripting document of Screen.dpi field for the details
            if (!Application.isEditor && Application.platform == RuntimePlatform.Android) {
                AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
 
                AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
                activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

                return (metrics.Get<float>("xdpi") + metrics.Get<float>("ydpi")) * 0.5f;
            }

            return Screen.dpi;
        }

        public override void Prepare(Level level) {
            DisableJoyPad();
            Enable = true;
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (_pointerID == NonePointerID) {
                _pointerID = eventData.pointerId;
                EnableJoyPadAt(eventData.position);
            }
        }

        private void EnableJoyPadAt(Vector2 position) {
            var camera = canvas.worldCamera;
            var viewPoint = camera.ScreenToViewportPoint(position);
            joyPad.anchorMin = viewPoint;
            joyPad.anchorMax = viewPoint;
            joyPad.gameObject.SetActive(true);
        }

        void IDragHandler.OnDrag(PointerEventData eventData) {
            if(eventData.pointerId == _pointerID) {
                Drag(eventData.pressPosition, eventData.position);
            }
        }
        
        private void Drag(Vector2 pressPosition, Vector2 pointerPosition) {
            var deltaPosition = pointerPosition - pressPosition;
            var direction = deltaPosition.normalized;
            var deltaInch = deltaPosition.magnitude / _dpi;
            var magnitude = Normalize(deltaInch, minDragInInches, maxDragInInches);
            _drag = direction * magnitude;
        }
        
        private float Normalize(float value, float minValue, float maxValue, float minNormalizedValue = 0, float maxNormalizedValue = 1)
        {
            return Mathf.Clamp(NormalizeUnclamped(value, minValue, maxValue, minNormalizedValue, maxNormalizedValue), minNormalizedValue, maxNormalizedValue);
        }
        
        private float NormalizeUnclamped(float value, float minValue, float maxValue, float minNormalizedValue = 0, float maxNormalizedValue = 1)
        {
            return (value - minValue) / (maxValue - minValue) * (maxNormalizedValue - minNormalizedValue) + minNormalizedValue;
        }
        
        public void OnPointerUp(PointerEventData eventData) {
            if (eventData.pointerId == _pointerID) {
                ReleaseInput();
            }
        }

        private void ReleaseInput() {
            _pointerID = NonePointerID;
            _drag = Vector2.zero;
            DisableJoyPad();
        }

        private void DisableJoyPad() {
            joyPad.gameObject.SetActive(false);
            joyPadAnimator.SetFloat(horizontalParamName, 0);
            joyPadAnimator.SetFloat(verticalParamName, 0);
        }

        private void Update() {
            if (_pointerID != NonePointerID) {
                UpdateJoyPadAnimation();
                if (_controller != null && _drag != Vector2.zero) {
                    _controller.Drag(_drag);
                }
            }
        }

        private void UpdateJoyPadAnimation() {
            joyPadAnimator.SetFloat(horizontalParamName,
                Mathf.MoveTowards(joyPadAnimator.GetFloat(horizontalParamName), _drag.x,
                    animSpeed * Time.deltaTime));
            joyPadAnimator.SetFloat(verticalParamName,
                Mathf.MoveTowards(joyPadAnimator.GetFloat(verticalParamName), _drag.y,
                    animSpeed * Time.deltaTime));
        }

        public override void OnLevelFinish(Level level) {
            base.OnLevelFinish(level);
            Enable = false;
        }
    }
}
