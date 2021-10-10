using UnityEngine;

namespace Solitaire
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] float minimumDragDistance = 10;

        bool inputAllowed = true;

        Camera cam;
        bool isDragging;
        Vector2 currentPoint;
        Vector2 clickedScreenPoint;
        Collider2D clickedCollider;

        void Awake()
        {
            cam = Camera.main;
            
            GameManager.OnNewGameDealing += DisableInput;
            CardMovement.OnCardMovementBegin += DisableInput;
            GameManager.OnNewGameDealt += EnableInput;
            CardMovement.OnCardMovementEnd += EnableInput;
        }

        void DisableInput() => inputAllowed = false;
        void EnableInput() => inputAllowed = true;

        void Update()
        {
            if (!inputAllowed) return;
            
            SetCurrentPointerLocation();

            if (Input.GetMouseButtonDown(0))
            {
                clickedScreenPoint = Input.mousePosition;
                clickedCollider = GetColliderUnderCursor();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isDragging) ProcessClick();
                else ProcessRelease();
                isDragging = false;
            }

            if (Input.GetMouseButton(0))
                isDragging = ((Vector2) Input.mousePosition - clickedScreenPoint).sqrMagnitude >
                             (minimumDragDistance * minimumDragDistance);
            if (isDragging) ProcessDrag();
        }

        void SetCurrentPointerLocation()
        {
            currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        Collider2D GetColliderUnderCursor()
        {
            return Physics2D.OverlapPoint(currentPoint, 1);
        }

        void ProcessClick()
        {
            FixPotentialStuckCollider();
            
            if (clickedCollider == null) return;
            if (clickedCollider.TryGetComponent<IClickable>(out IClickable clickedSprite)) clickedSprite.Click();
        }

        void ProcessDrag()
        {
            if (clickedCollider == null) return;
            if (clickedCollider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Drag(currentPoint);
        }

        void ProcessRelease()
        {
            if (clickedCollider == null) return;

            Collider2D releaseCollider = GetColliderUnderCursor();
            if (releaseCollider == null) releaseCollider = clickedCollider;
            if (clickedCollider.TryGetComponent<IDraggable>(out IDraggable dragged)) dragged.Release(releaseCollider);
        }
        void FixPotentialStuckCollider()
        {
            Collider2D potentialStuckCollider =  Physics2D.OverlapPoint(currentPoint, 8);
            if (potentialStuckCollider == null) return;
            if (potentialStuckCollider.TryGetComponent(out IDraggable dragged)) dragged.Release(potentialStuckCollider);
        }
        
    }
}