using UnityEngine;

namespace Solitaire
{
    public class MouseDown
    {
        public Vector2 ScreenPoint => _screenPoint;
        readonly Vector2 _screenPoint;

        public Vector2 WorldPoint => _worldPoint;
        readonly Vector2 _worldPoint;

        public Collider2D Collider => _collider;
        readonly Collider2D _collider;

        public Vector2 ColliderInitialPosition => _colliderInitialPosition;
        readonly Vector2 _colliderInitialPosition;

        public MouseDown(Camera mainCamera)
        {
            _screenPoint = Input.mousePosition;
            _worldPoint = mainCamera.ScreenToWorldPoint(_screenPoint);
            _collider = GetColliderUnderCursor();
            if (_collider != null) _colliderInitialPosition = _collider.transform.position;
        }
        
        Collider2D GetColliderUnderCursor() => Physics2D.OverlapPoint(_worldPoint, 1);
        
    }
}