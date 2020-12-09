using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    public float rotationSpeed = 1;
    public ParticleSystem explosionParticles;

    public float lifeSpan = 10f;
    public float explosionRadius = 4;
    public float explosionPower = 10;
    public AnimationCurve explosionImpactCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public Vector3 axisAdjustment = Vector3.one; // This property defines how fast to travel through axes.
    
    private Transform target;
    private float distanceFromTarget;
    [HideInInspector] public RocketLauncher launcher;
    private Rigidbody rb;
    private float timer;

    private float previousDifference;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) Explode();
        
        Vector3 direction = (target.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(direction).eulerAngles;
        var rot = transform.rotation.eulerAngles;
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(targetRotation) * Vector3.forward, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
    
        float angleDifference = Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation));
        float delta = Time.deltaTime * rotationSpeed * Mathf.Exp(angleDifference % 180 / 180) *
                      (1 + (lifeSpan - timer) / lifeSpan) * (2f - Mathf.Abs(angleDifference - previousDifference) / 180) *
            (2f - Vector3.Distance(transform.position, target.position) / distanceFromTarget);
        
        Quaternion newRotation = Quaternion.identity;
        int[] arr = {1, 0, 2};
        foreach (var i in arr)
        {
            Vector3 axis = Vector3.zero;
            axis[i] = 1;
            newRotation *= Quaternion.RotateTowards(Quaternion.AngleAxis(rot[i], axis), Quaternion.AngleAxis(targetRotation[i], axis), delta * axisAdjustment[i]);
        }
        
        transform.rotation = newRotation;
        rb.velocity = rb.velocity.magnitude * transform.forward;
        previousDifference = angleDifference;
    }
    


    void FixedUpdate()
    {
        
        // transform.rotation = Quaternion.Slerp(transform.rotation,
        // Quaternion.AngleAxis(Mathf.Acos(Vector3.Dot(Vector3.forward, direction)) * Mathf.Rad2Deg, Vector3.Cross(Vector3.forward, direction)),
        // Time.fixedDeltaTime * rotationSpeed);
        
        // rb.velocity = rb.velocity.magnitude * transform.forward;
    }

    public void Launch(Transform spawnPoint, Transform _target, float launchForce, float _speed)
    {
        target = _target;
        timer = lifeSpan;
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        gameObject.SetActive(true);
        rb.AddForce(launchForce * Vector3.forward);
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] hits = new Collider[10];
        var successfulHits = new List<IDamageable>();
        int count = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hits);
        for (int i = 0; i < count; i++)
        {
            var hit = hits[i];
            IDamageable _hit = hit.gameObject.GetComponent<IDamageable>();
            if (_hit == null) continue;
            successfulHits.Add(_hit);
            float t = Mathf.Clamp01(Vector3.Distance(transform.position, hit.transform.position) / explosionRadius);
            _hit.GetHit(Mathf.Clamp01(explosionImpactCurve.Evaluate(t)) * (1 + explosionPower / 100));
        }

        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        launcher.AfterRocketDie(this, successfulHits);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
