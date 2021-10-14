using UnityEngine;

namespace Solitaire
{
    public class MouseDown
    {
        public Vector2 ScreenPoint => screenPoint;
        Vector2 screenPoint;

        public Vector2 WorldPoint => worldPoint;
        Vector2 worldPoint;

        public Collider2D Collider => collider;
        Collider2D collider;

        public Vector2 ColliderInitialPosition => colliderInitialPosition;
        Vector2 colliderInitialPosition;

        public MouseDown(Camera mainCamera)
        {
            screenPoint = Input.mousePosition;
            worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            collider = GetColliderUnderCursor();
            if (collider != null) colliderInitialPosition = collider.transform.position;
        }
        
        Collider2D GetColliderUnderCursor() => Physics2D.OverlapPoint(worldPoint, 1);
        
    }
}