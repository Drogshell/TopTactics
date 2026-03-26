using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float turnSpeed;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) > stoppingDistance)
        {
            var moveDirection = (targetPos - transform.position).normalized; // To get rid of magnitude we normalise
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);
            unitAnimator.SetBool("isWalking", true);
        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
        }
    }

    public void Move(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
