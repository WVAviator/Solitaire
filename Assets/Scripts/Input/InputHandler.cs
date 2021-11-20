using System;
using UnityEngine;

namespace Solitaire
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] float minimumDragDistance = 10;
        [SerializeField] float doubleClickSpeed = 0.1f;
        [SerializeField] GameObject _settingsPanel;

        bool _inputAllowed = true;

        Camera _mainCamera;
        bool _isDragging;
        Vector2 _currentMouseWorldPosition;
        float _lastClickTime;

        MouseDown _mouseDown;

        void Awake() => _mainCamera = Camera.main;
        void OnEnable()
        {
            CardAnimation.OnCardAnimationBegins += DisableInput;
            CardAnimation.OnCardAnimationEnds += EnableInput;
        }
        void OnDisable()
        {
            CardAnimation.OnCardAnimationBegins -= DisableInput;
            CardAnimation.OnCardAnimationEnds -= EnableInput;
        }

        void DisableInput() => _inputAllowed = false;
        void EnableInput() => _inputAllowed = true;

        void Update()
        {
            if (!_inputAllowed || _settingsPanel.activeSelf) return;
            
            SetCurrentMouseWorldPosition();

            if (Input.GetMouseButtonDown(0)) _mouseDown = new MouseDown(_currentMouseWorldPosition);
            
            if (Input.GetMouseButtonUp(0))
            {
                if (!_isDragging && !IsDoubleClick()) ProcessClick();
                else if (!_isDragging && IsDoubleClick()) ProcessDoubleClick();
                else ProcessRelease();
                _isDragging = false;
                _lastClickTime = Time.time;
            }

            if (Input.GetMouseButton(0)) _isDragging = MovedEnoughToBeConsideredDragging();
            if (_isDragging) ProcessDrag();
        }

        bool IsDoubleClick() => Time.time - _lastClickTime < doubleClickSpeed;

        bool MovedEnoughToBeConsideredDragging() =>
            ((Vector2)Input.mousePosition - _mouseDown.ClickedScreenPosition).sqrMagnitude >
            (minimumDragDistance * minimumDragDistance);

        void SetCurrentMouseWorldPosition() => 
            _currentMouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);


        void ProcessClick() => _mouseDown.Click(_currentMouseWorldPosition);
        void ProcessDoubleClick() => _mouseDown.DoubleClick(_currentMouseWorldPosition);
        void ProcessDrag() => _mouseDown.DragTo(_currentMouseWorldPosition);
        void ProcessRelease() => _mouseDown.Release();

    }
}