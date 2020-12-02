using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public PathMovement pathMovement;

    private Animator animator;
    private int jumpAnimationCode;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        jumpAnimationCode = Animator.StringToHash("Jump");
    }

    // Update is called once per frame
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
            animator.SetTrigger(jumpAnimationCode);
        }
    }
}
