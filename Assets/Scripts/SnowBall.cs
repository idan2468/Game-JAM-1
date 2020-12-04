using UnityEngine;

[RequireComponent(typeof(PathMovement))]
public class SnowBall : MonoBehaviour
{
    public float DamagePower = 10;
    public float rollingSpeed = 2f;
    public GameObject ball;
    private PathMovement path;
    [HideInInspector] public SnowballSpawner spawner;

    private void Awake()
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
        spawner.AfterSnowballDie(this);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        path.SetPosition(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hit = other.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.GetHit(DamagePower / 100);
        }
    }
}
