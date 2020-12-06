using System;
using UnityEngine;

public enum PlayerIndex
{
    Player1 = 1,
    Player2 = 2
}
public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed = 500;
    public float rotationSpeed = 5;
    public float damageResistance = 0;
    public PlayerIndex playerIndex;

    float verticalMovement;
    private RocketLauncher rocketLauncher;
    private bool isJumping;
    private string horizontalIn, verticalIn, jumpIn, fireIn;
    private Rigidbody rb;

    void Start()
    {
        rocketLauncher = GetComponent<RocketLauncher>();
        rb = GetComponent<Rigidbody>();
        
        horizontalIn = "Horizontal_" + playerIndex;
        verticalIn = "Vertical_" + playerIndex;
        jumpIn = "Jump_" + playerIndex;
        fireIn = "Fire_" + playerIndex;
    }

    void Update()
    {
        verticalMovement = Input.GetAxisRaw(verticalIn);
        float horizontalMovement = Input.GetAxisRaw(horizontalIn);
        transform.Rotate(Vector3.up, horizontalMovement * rotationSpeed);
        rb.velocity = speed * verticalMovement * transform.forward;

        // if (Input.GetAxis(jumpIn) > Mathf.Epsilon)
        // {
        //     Jump();
        // }
        //
        // if (Input.GetButtonDown(fireIn))
        // {
        //     Fire();
        // }
    }


    private void Fire()
    {
        rocketLauncher.Launch();
    }



    public void Jump()
    {
        if (isJumping) return;
        isJumping = true;
        // LeanTween.moveLocal(body, jumpForce * Vector3.up, jumpDuration).setLoopPingPong(1)
        //     .setEaseOutQuad().setOnComplete(() => { isJumping = false; });
    }

    public void GetHit(float power)
    {
        // Respawn(power * (1 - damageResistance / 100f));
        UIController.getInstance().UpdateDamageGUI(playerIndex, power);
    }
}
