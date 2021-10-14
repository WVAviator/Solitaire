using System;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    bool animationActive;
    Vector3 targetPosition;

    public static event Action OnCardAnimationBegins;
    public static event Action OnCardAnimationEnds;

    void Update()
    {
        if (!animationActive) return;
        if (transform.position == targetPosition)
        {
            FinishAnimation();
            return;
        }
        
        AnimateMovement();
    }

    void AnimateMovement()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (IsCloseEnough()) transform.position = targetPosition;
    }

    bool IsCloseEnough() => (transform.position - targetPosition).sqrMagnitude < 0.001f;

    void FinishAnimation()
    {
        OnCardAnimationEnds?.Invoke();
        animationActive = false;
    }

    public void SetAnimationTargetPosition(Vector3 position)
    {
        targetPosition = position;
        animationActive = true;
        OnCardAnimationBegins?.Invoke();
    }
}
