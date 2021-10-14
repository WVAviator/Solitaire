using System;
using UnityEngine;

namespace Solitaire
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] float minimumDragDistance = 10;

        bool inputAllowed = true;

        Camera mainCamera;
        bool isDragging;
        Vector2 currentPoint;

        MouseDown mouseDown;

        void Awake() => mainCamera = Camera.main;
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

        void DisableInput() => inputAllowed = false;
        void EnableInput() => inputAllowed = true;

        void Update()
        {
            if (!inputAllowed) return;
            
            SetCurrentPointerLocation();

            if (Input.GetMouseButtonDown(0)) mouseDown = new MouseDown(mainCamera);
            

            if (Input.GetMouseButtonUp(0))
            {
                if (!isDragging) ProcessClick();
                else ProcessRelease();
                isDragging = false;
            }

            if (Input.GetMouseButton(0)) isDragging = HasDraggedFarEnough();
            
            if (isDragging) ProcessDrag();
        }

        bool HasDraggedFarEnough() =>
            ((Vector2) Input.mousePosition - mouseDown.ScreenPoint).sqrMagnitude >
            (minimumDragDistance * minimumDragDistance);

        void SetCurrentPointerLocation()
        {
            currentPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        Collider2D GetColliderUnderCollider()
        {
            return Physics2D.OverlapPoint(mouseDown.Collider.transform.position, 1);
        }

        void ProcessClick()
        {
            FixPotentialStuckCollider();
            
            if (mouseDown.Collider == null) return;
            if (mouseDown.Collider.TryGetComponent<IClickable>(out IClickable clickedSprite)) clickedSprite.Click();
        }

        void ProcessDrag()
        {
            if (mouseDown.Collider == null) return;
            Vector2 clickedPositionOffset = mouseDown.WorldPoint - mouseDown.ColliderInitialPosition;
            if (mouseDown.Collider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Drag(currentPoint, clickedPositionOffset);
        }

        void ProcessRelease()
        {
            if (mouseDown.Collider == null) return;

            Collider2D releaseCollider = GetColliderUnderCollider();
            if (releaseCollider == null) releaseCollider = mouseDown.Collider;
            if (mouseDown.Collider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Release(releaseCollider);
        }
        void FixPotentialStuckCollider()
        {
            Collider2D potentialStuckCollider =  Physics2D.OverlapPoint(currentPoint, 8);
            if (potentialStuckCollider == null) return;
            if (potentialStuckCollider.TryGetComponent(out IDraggable dragged)) dragged.Release(potentialStuckCollider);
        }
        
    }
}