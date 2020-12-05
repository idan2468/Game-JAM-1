using UnityEngine;

public enum PlayerIndex
{
    Player1 = 1,
    Player2 = 2
}

[RequireComponent(typeof(PathMovement), typeof(RocketLauncher))]
public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float jumpDuration;
    public GameObject body;
    public float damageResistance = 0;
    public PlayerIndex playerIndex;

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
        float horizontalMovement = Input.GetAxis("Horizontal_" + playerIndex);
        if (horizontalMovement > Mathf.Epsilon)
        {
            pathMovement.MoveForward();
        }

        if (horizontalMovement < -Mathf.Epsilon)
        {
            pathMovement.MoveBackwards();
        }

        if (Input.GetAxis("Jump_" + playerIndex) > Mathf.Epsilon)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire_" + playerIndex))
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
        UIController.getInstance().UpdateDamageGUI(playerIndex, power);
    }
}
