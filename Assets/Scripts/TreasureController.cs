using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private  int animatorOpenParam = Animator.StringToHash("OpenChest");

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger(animatorOpenParam);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: " + other.name);
    }
}
