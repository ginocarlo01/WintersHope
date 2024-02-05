using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithLimits : MonoBehaviour
{
    [Header("Position Variables")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothing = 5.0f;
    [SerializeField]
    Vector2 maxPosition;
    [SerializeField]
    Vector2 minPosition;

    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);

        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

    public void AddMinPosition(Vector2 plusValue)
    {
        minPosition += plusValue;
    }

    public void AddMaxPosition(Vector2 plusValue)
    {
        maxPosition += plusValue;
    }

    public void BeginKick()
    {
        animator.SetBool("kick_active", true);
    }

    public void StopKick()
    {
        animator.SetBool("kick_active", false);
    }

    #region SubjectSubscription
    private void OnEnable()
    {
        RoomTransition.PlayerEnteredTransition += AddMaxPosition;
        RoomTransition.PlayerEnteredTransition += AddMinPosition;
    }
    private void OnDisable()
    {
        RoomTransition.PlayerEnteredTransition -= AddMaxPosition;
        RoomTransition.PlayerEnteredTransition -= AddMinPosition;
    }
    #endregion
}
