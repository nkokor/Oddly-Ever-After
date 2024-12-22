using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cam;

    public float baseSpeed = 1f;
    public float maxSpeed = 6f;
    public float acceleration = 0.5f;
    public float turnSmoothTime = 0.1f;

    private float currentSpeed;
    private float turnSmoothVelocity;

    public Animator animator;

    private bool isJumping = false;

    void Start()
    {
        currentSpeed = 0f;
        agent.speed = baseSpeed;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = direction.magnitude >= 0.1f;

        if (isMoving)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            agent.speed = currentSpeed;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            agent.SetDestination(transform.position + moveDirection);
        }
        else
        {
            currentSpeed = 0f;
            agent.speed = 0f;
        }

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnFallToIdleTransition()
    {
        isJumping = false;
        animator.SetBool("IsJumping", false);
    }
}
