using UnityEngine;

[RequireComponent(typeof(PathMovement))]
public class SnowBall : MonoBehaviour
{
    public float DamagePower = 10;
    public float rollingSpeed = 2f;
    public GameObject ball;
    private PathMovement path;

    private void Start()
    {
        path = GetComponent<PathMovement>();
    }

    void Update()
    {
        ball.transform.Rotate(Vector2.right, rollingSpeed);
        path.MoveBackwards();
        if (path.T - Mathf.Epsilon <= 0) Despawn();
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        path.SetPosition(1);
    }

    private void OnCollisionEnter(Collision other)
    {
        IDamageable hit = other.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.GetHit(DamagePower / 100);
        }
    }
}
