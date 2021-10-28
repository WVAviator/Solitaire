using UnityEngine;

namespace Solitaire
{
    public class MouseDown
    {
        public Vector2 ClickedScreenPosition { get; }
        
        readonly Vector2 _clickedWorldPosition;
        readonly Collider2D _collider;
        readonly Vector2 _colliderInitialPosition;

        public MouseDown(Vector2 clickedWorldPosition)
        {
            ClickedScreenPosition = Input.mousePosition;
            _clickedWorldPosition = clickedWorldPosition;
            _collider = GetCollider(_clickedWorldPosition, 1);
            if (_collider != null) _colliderInitialPosition = _collider.transform.position;
        }

        public void DragTo(Vector2 currentMousePosition)
        {
            if (_collider == null) return;
            Vector2 clickedPositionOffset = _clickedWorldPosition - _colliderInitialPosition;
            if (_collider.TryGetComponent(out IDraggable dragged)) dragged.Drag(currentMousePosition, clickedPositionOffset);
        }

        public void Release()
        {
            if (_collider == null) return;

            Collider2D releaseCollider = GetCollider(_collider.transform.position, 1);
            if (releaseCollider == null) releaseCollider = _collider;
            if (_collider.TryGetComponent(out IDraggable dragged)) dragged.Release(releaseCollider);
        }

        public void Click(Vector2 currentMousePosition)
        {
            FixPotentialStuckCollider(currentMousePosition);
            
            if (_collider == null) return;
            if (_collider.TryGetComponent(out IClickable clickedSprite)) clickedSprite.Click();
        }

        Collider2D GetCollider(Vector3 position, int layer) => Physics2D.OverlapPoint(position, layer);

        void FixPotentialStuckCollider(Vector2 currentMousePosition)
        {
            Collider2D potentialStuckCollider = GetCollider(currentMousePosition, 8);
            if (potentialStuckCollider == null) return;
            if (potentialStuckCollider.TryGetComponent(out IDraggable dragged)) dragged.Release(potentialStuckCollider);
        }
        
    }
}