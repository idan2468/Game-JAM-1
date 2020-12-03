using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float jumpDuration;
    public GameObject body;
    public float damageResistance = 0;
    
    private PathMovement pathMovement;
    private bool isJumping;
    void Start()
    {
        pathMovement = GetComponent<PathMovement>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pathMovement.MoveForward();
        }
    
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pathMovement.MoveBackwards();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Fire();
        }
    }

    private void Fire()
    {
        
    }

    public void Respawn(float distance)
    {
        pathMovement.TeleportBack(distance);
    }

    public void Jump()
    {
        if (isJumping) return;
        isJumping = true;
        LeanTween.moveLocal(body, jumpForce * Vector3.up, jumpDuration).setLoopPingPong(1)
            .setEaseOutQuad().setOnComplete(() => {isJumping = false;});
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageMaker obs = other.GetComponent<IDamageMaker>();
        if (obs == null) return;

        float impact = obs.MakeDamage(transform);
        Respawn(impact * (1 - damageResistance / 100f));
    }
}
