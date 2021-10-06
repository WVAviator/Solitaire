using UnityEngine;

namespace Solitaire
{
    public interface IDraggable
    {
        void Drag(Vector2 updatedPosition);
        void Release(Collider2D col);
    }
}