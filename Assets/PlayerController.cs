using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float jumpDuration;
    public GameObject body;
    private PathMovement pathMovement;
    private bool isJumping;
    // private Animator animator;
    // private int jumpAnimationCode;
    void Start()
    {
        pathMovement = GetComponent<PathMovement>();
        // animator = GetComponentInChildren<Animator>();
        // jumpAnimationCode = Animator.StringToHash("Jump");
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
            Jump();

        }
    }

    public void Jump()
    {
        if (isJumping) return;
        isJumping = true;
        LeanTween.moveLocal(body, jumpForce * Vector3.up, jumpDuration).setLoopPingPong(1)
            .setEaseOutQuad().setOnComplete(() => {isJumping = false;});

    }
}
