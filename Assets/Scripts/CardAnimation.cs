using System;
using UnityEngine;

namespace Solitaire
{
    public class CardAnimation : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1;
        bool _animationActive;
        Vector3 _targetPosition;

        public static event Action OnCardAnimationBegins;
        public static event Action OnCardAnimationEnds;

        public void SetAnimationTargetPosition(Vector3 position)
        {
            _targetPosition = position;
            _animationActive = true;
            OnCardAnimationBegins?.Invoke();
        }

        void Update()
        {
            if (!_animationActive) return;
            if (transform.position == _targetPosition)
            {
                FinishAnimation();
                return;
            }

            AnimateMovement();
        }

        void FinishAnimation()
        {
            OnCardAnimationEnds?.Invoke();
            _animationActive = false;
        }

        void AnimateMovement()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            if (IsCloseEnough()) transform.position = _targetPosition;
        }

        bool IsCloseEnough() => (transform.position - _targetPosition).sqrMagnitude < 0.001f;
    }
}