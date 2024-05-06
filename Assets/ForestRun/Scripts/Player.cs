using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RoadManager _roadManager;

    private CharacterController controller;
    private Vector3 move;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2f;//The distance between tow lanes 

    public float jumpForce;
    public Animator animator;
    public float Gravity = -20;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (SwipeManager.swipeUp)
        {
            animator.SetBool("jump", true);
            Jump();
        }
        else
        {
            animator.SetBool("jump", false);
            move.y += Gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPostion = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPostion += Vector3.left * laneDistance;
        }

        else if (desiredLane == 2)
        {
            targetPostion += Vector3.right * laneDistance;
        }

        transform.position = targetPostion;
    }

    private void OnTriggerEnter(Collider other)
    {
        _roadManager.Spawn();
    }

    private void FixedUpdate()
    {
        controller.Move(move * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        move.y = jumpForce;
    }
}
