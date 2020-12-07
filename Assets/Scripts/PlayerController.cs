using System;
using UnityEngine;

public enum PlayerIndex
{
    Player1 = 1,
    Player2 = 2
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDamageable
{
    public float jumpForce;
    public float speed = 5;
    public float rotationSpeed = 5;
    public PlayerIndex playerIndex;

    [SerializeField] private Camera cam;
    public float gravity = 9.8f;
    
    private RocketLauncher rocketLauncher;
    private bool isGrounded;
    private string horizontalIn, verticalIn, jumpIn, fireIn;
    private CharacterController controller;
    private float verticalSpeed;
    private int lifePoints;

    void Start()
    {
        lifePoints = 3;
        rocketLauncher = GetComponentInChildren<RocketLauncher>();
        controller = GetComponent<CharacterController>();
        
        horizontalIn = "Horizontal_" + playerIndex;
        verticalIn = "Vertical_" + playerIndex;
        jumpIn = "Jump_" + playerIndex;
        fireIn = "Fire_" + playerIndex;
    }

    void Update()
    {

        var vertical = Input.GetAxis(verticalIn);
        var horizontal = Input.GetAxis(horizontalIn);
        var dir = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = PlayerMove(dir).normalized * speed;
        if (controller.isGrounded)
        {
            verticalSpeed = 0;  // When grounded, none velocity in y axis.
            if (Input.GetAxis(jumpIn) > Mathf.Epsilon)
                verticalSpeed = jumpForce;
        }

        verticalSpeed -= gravity * Time.deltaTime;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown(fireIn))
        {
            Fire();
        }
    }

    private Vector3 PlayerMove(Vector3 dir)
    {
        if (dir.magnitude <= .1) return Vector3.zero;

        var forwardAccordingToCamera = Quaternion.Euler(0f, cam.gameObject.transform.eulerAngles.y, 0f) * dir;
        var rotation = Quaternion.LookRotation(forwardAccordingToCamera);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);

        return forwardAccordingToCamera;
    }



    private void Fire()
    {
        Debug.Log("fire!");
        rocketLauncher.Launch();
    }
    

    public void GetHit(float power)
    {
        UIController.getInstance().setLifeGUI(playerIndex, --lifePoints);
    }
}
