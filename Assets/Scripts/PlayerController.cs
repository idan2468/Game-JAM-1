using UnityEngine;

[RequireComponent(typeof(PathMovement), typeof(RocketLauncher))]
public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float jumpDuration;
    public GameObject body;
    public float damageResistance = 0;


    private RocketLauncher rocketLauncher;
    private PathMovement pathMovement;
    private bool isJumping;
    void Start()
    {
        pathMovement = GetComponent<PathMovement>();
        rocketLauncher = GetComponent<RocketLauncher>();
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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Fire();
        }
    }

    private void Fire()
    {
        rocketLauncher.Launch();
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
            .setEaseOutQuad().setOnComplete(() => { isJumping = false; });
    }

    public void GetHit(float power)
    {
        Respawn(power * (1 - damageResistance / 100f));
    }
}
