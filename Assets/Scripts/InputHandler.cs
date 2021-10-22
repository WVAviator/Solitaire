using System;
using UnityEngine;

namespace Solitaire
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] float minimumDragDistance = 10;

        bool _inputAllowed = true;

        Camera _mainCamera;
        bool _isDragging;
        Vector2 _currentPoint;

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
            if (!_inputAllowed) return;
            
            SetCurrentPointerLocation();

            if (Input.GetMouseButtonDown(0)) _mouseDown = new MouseDown(_mainCamera);
            

            if (Input.GetMouseButtonUp(0))
            {
                if (!_isDragging) ProcessClick();
                else ProcessRelease();
                _isDragging = false;
            }

            if (Input.GetMouseButton(0)) _isDragging = HasDraggedFarEnough();
            
            if (_isDragging) ProcessDrag();
        }

        bool HasDraggedFarEnough() =>
            ((Vector2) Input.mousePosition - _mouseDown.ScreenPoint).sqrMagnitude >
            (minimumDragDistance * minimumDragDistance);

        void SetCurrentPointerLocation()
        {
            _currentPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        Collider2D GetColliderUnderCollider()
        {
            return Physics2D.OverlapPoint(_mouseDown.Collider.transform.position, 1);
        }

        void ProcessClick()
        {
            FixPotentialStuckCollider();
            
            if (_mouseDown.Collider == null) return;
            if (_mouseDown.Collider.TryGetComponent<IClickable>(out IClickable clickedSprite)) clickedSprite.Click();
        }

        void ProcessDrag()
        {
            if (_mouseDown.Collider == null) return;
            Vector2 clickedPositionOffset = _mouseDown.WorldPoint - _mouseDown.ColliderInitialPosition;
            if (_mouseDown.Collider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Drag(_currentPoint, clickedPositionOffset);
        }

        void ProcessRelease()
        {
            if (_mouseDown.Collider == null) return;

            Collider2D releaseCollider = GetColliderUnderCollider();
            if (releaseCollider == null) releaseCollider = _mouseDown.Collider;
            if (_mouseDown.Collider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Release(releaseCollider);
        }
        void FixPotentialStuckCollider()
        {
            Collider2D potentialStuckCollider =  Physics2D.OverlapPoint(_currentPoint, 8);
            if (potentialStuckCollider == null) return;
            if (potentialStuckCollider.TryGetComponent(out IDraggable dragged)) dragged.Release(potentialStuckCollider);
        }
        
        public void QuitGame()
        {
#if UNITY_WEBGL
            return;
#endif
            
            Application.Quit();
        }
        
    }
}