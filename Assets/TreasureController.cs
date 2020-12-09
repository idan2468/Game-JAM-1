using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private  int animatorOpenParam = Animator.StringToHash("needToOpen");

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(animatorOpenParam,true);
        }
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.collider.gameObject.CompareTag("Player"))
    //     {
    //         animator.SetBool(animatorOpenParam,true);
    //     }
    // }
    //
    // private void OnCollisionEnter(Collision other)
    // { 
    //     if (other.collider.gameObject.CompareTag("Player"))
    //     {
    //         animator.SetBool(animatorOpenParam,true);
    //     }
    // }
}
