using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    public Transform target;
    public Transform respawnPoint;
    public float launchForce = 100f;
    public float speed = 3f;
    public float rotationSpeed = 1;
    public ParticleSystem explosionParticles;
    
    public float explosionRadius = 4;
    public float explosionPower = 10;
    public AnimationCurve explosionImpactCurve = AnimationCurve.Linear(0,0,1,1);
    
    private Rigidbody rb;
    public void Launch()
    {
        rb.velocity = Vector3.zero;
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
        
        gameObject.SetActive(true);
        rb.AddForce(launchForce * Vector3.forward);
    }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.AngleAxis(Mathf.Acos(Vector3.Dot(Vector3.forward, direction))*Mathf.Rad2Deg, Vector3.Cross(Vector3.forward, direction)), 
            Time.fixedDeltaTime * rotationSpeed);
        rb.velocity = Mathf.Lerp(rb.velocity.magnitude, speed, Time.fixedDeltaTime) * transform.forward;
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] hits = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hits);
        for (int i = 0; i < count; i++) 
        {
            var hit = hits[i];
            IDamageable _hit = hit.gameObject.GetComponent<IDamageable>();
            if (_hit == null) continue;
            float t = Mathf.Clamp01(Vector3.Distance(transform.position, hit.transform.position) / explosionRadius);
            _hit.GetHit(Mathf.Clamp01(explosionImpactCurve.Evaluate(t)) * (1 + explosionPower/100));
        }

        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
