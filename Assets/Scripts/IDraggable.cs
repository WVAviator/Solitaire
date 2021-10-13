using UnityEngine;

namespace Solitaire
{
    public interface IDraggable
    {
        void Drag(Vector2 updatedPosition, Vector2 clickedPositionOffset);
        void Release(Collider2D col);
    }
}