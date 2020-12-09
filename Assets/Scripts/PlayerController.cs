using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerIndex
{
    Player1,
    Player2
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private GameObject playerCoordinateSystem;
    [SerializeField] private Camera cam;

    [Header("Settings")]
    public PlayerIndex playerIndex;
    [SerializeField] private bool relativeToCamera = true;
    public float jumpForce;
    public float speed = 5;
    public float rotationSpeed = 5;
    public float fireCooldown = 3f;
    public float gravity = 9.8f;
    public Vector2 airResistance = new Vector2(5f, 5f);


    private RocketLauncher rocketLauncher;
    private bool isGrounded;
    private string horizontalIn, verticalIn, jumpIn, fireIn;
    private CharacterController controller;
    private float verticalSpeed;
    private Vector3 impactVelocity = Vector3.zero;
    private float fireCooldownTimer;

    void Start()
    {
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
            verticalSpeed = 0; // When grounded, none velocity in y axis.
            if (Input.GetAxis(jumpIn) > Mathf.Epsilon)
                verticalSpeed = jumpForce;
        }
        impactVelocity = Vector3.ClampMagnitude(impactVelocity - new Vector3(airResistance.x, 0, airResistance.y)*Time.deltaTime, 0);

        verticalSpeed -= gravity * Time.deltaTime;
        velocity.y = verticalSpeed;
        velocity += impactVelocity;
        controller.Move(velocity * Time.deltaTime);
        fireCooldownTimer = Mathf.Max(fireCooldownTimer - Time.deltaTime, 0);
        
        if (Input.GetButtonDown(fireIn))
        {
            Fire();
        }
        UIController.Instance.UpdatePlayerCooldownSlider(playerIndex, (fireCooldown - fireCooldownTimer) / fireCooldown);
    }

    
    private Vector3 PlayerMove(Vector3 dir)
    {
        if (dir.magnitude <= .1) return Vector3.zero;
        var forwardDir = relativeToCamera
            ? cam.gameObject.transform.eulerAngles.y
            : playerCoordinateSystem.transform.eulerAngles.y;
        var forwardAccordingToCamera = Quaternion.Euler(0f, forwardDir, 0f) * dir;
        // forwardAccordingToCamera = dir; 
        var rotation = Quaternion.LookRotation(forwardAccordingToCamera);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        return forwardAccordingToCamera;
    }


    private void Fire()
    {
        if (fireCooldownTimer <= 0)
        {
            fireCooldownTimer = fireCooldown;
            rocketLauncher.Launch();
        }
    }

 

    public void GetHit(float power, Transform hit)
    {
        Vector3 direction = hit.forward;
        impactVelocity += power * direction;
        Debug.Log(impactVelocity);
    }
    
}