using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public PathMovement pathMovement;

    private bool isJumping;
    private Animator animator;
    private int jumpAnimationCode;
    void Start()
    {
        animator = GetComponent<Animator>();
        jumpAnimationCode = Animator.StringToHash("Jump");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pathMovement.MoveForward();
        }
    
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pathMovement.MoveBackwards();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                animator.SetTrigger(jumpAnimationCode);
                isJumping = true;
            }

        }
    }
}
