using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


// [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GameObject playerCoordinateSystem;

    [SerializeField] private Camera cam;

    [Header("Settings")] public PlayerIndex playerIndex;
    [SerializeField] private bool relativeToCamera = true;
    public float jumpForce;
    public float speed = 5;
    public float rotationSpeed = 5;
    public float fireCooldown = 3f;
    public float gravity = 9.8f;

    private RocketLauncher rocketLauncher;
    private string horizontalIn, verticalIn, jumpIn, fireIn;
    private CharacterController controller;
    private float verticalSpeed;
    private float fireCooldownTimer;
    private Animator animator;

    private int velocityID_animator;
    private int isGroundedID_animator;
    private int fireID_animator;

    void Start()
    {
        UIController.Instance.playersSliders[(int) playerIndex] = GetComponentInChildren<Slider>();

        rocketLauncher = GetComponentInChildren<RocketLauncher>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        velocityID_animator = Animator.StringToHash("Velocity");
        isGroundedID_animator = Animator.StringToHash("IsGrounded");
        fireID_animator = Animator.StringToHash("Fire");

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
        animator.SetFloat(velocityID_animator, velocity.magnitude);
        velocity.y = verticalSpeed;
        animator.SetBool(isGroundedID_animator, controller.isGrounded);

        if (controller.isGrounded)
        {
            if (verticalSpeed < 0) verticalSpeed = -1; // When grounded, none velocity in y axis.
            if (Input.GetAxisRaw(jumpIn) > .1f)
            {
                verticalSpeed = jumpForce;

                // Sound Test without position
                MusicController.Instance.PlaySound(MusicController.SoundEffects.Jump);
            }
        }

        verticalSpeed -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        fireCooldownTimer = Mathf.Max(fireCooldownTimer - Time.deltaTime, 0);
        if (Input.GetButtonDown(fireIn))
        {
            Fire();
        }

        UIController.Instance.UpdatePlayerCooldownSlider(playerIndex,
            (fireCooldown - fireCooldownTimer) / fireCooldown);
    }

    private Vector3 PlayerMove(Vector3 dir)
    {
        if (dir.magnitude <= .1) return Vector3.zero;
        var forwardDir = relativeToCamera
            ? cam.gameObject.transform.eulerAngles.y
            : playerCoordinateSystem.transform.eulerAngles.y;
        var forwardAccordingToCamera = Quaternion.Euler(0f, forwardDir, 0f) * dir;
        var rotation = Quaternion.LookRotation(forwardAccordingToCamera);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        return forwardAccordingToCamera;
    }


    private void Fire()
    {
        if (Time.timeScale < 0.01) return;
        if (fireCooldownTimer <= 0)
        {
            fireCooldownTimer = fireCooldown;
            rocketLauncher.Launch();
            animator.SetTrigger(fireID_animator);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Treasure"))
        {
            GameManager.Instance.OnPlayerWin(playerIndex);
        }
    }
}