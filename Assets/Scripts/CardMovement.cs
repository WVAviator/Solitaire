using System;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    bool moveTowardsTarget;
    Vector3 targetPosition;

    public static event Action OnCardMovementBegin;
    public static event Action OnCardMovementEnd;

    void Update()
    {
        if (!moveTowardsTarget) return;
        if (transform.position == targetPosition)
        {
            OnCardMovementEnd?.Invoke();
            return;
        }
        
        Move(targetPosition);
    }

    void Move(Vector3 target)
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if ((transform.position - target).sqrMagnitude < 0.001f) transform.position = target;

    }

    public void SetNewTargetPosition(Vector3 position)
    {
        targetPosition = position;
        moveTowardsTarget = true;
        OnCardMovementBegin?.Invoke();
    }
}
