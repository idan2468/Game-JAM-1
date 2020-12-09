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

    
    private Transform target;
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
        var targetRotation = Quaternion.LookRotation(direction);
        var rot = transform.rotation;
        
        Debug.DrawLine(transform.position, transform.position + targetRotation * Vector3.forward, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
    
        float angleDifference = Quaternion.Angle(rot, (targetRotation));
        float delta = Time.deltaTime * rotationSpeed;
        delta *= Mathf.Exp(angleDifference % 180 / 180);
        delta *= 1 + (lifeSpan - timer) / lifeSpan;
        delta *= 2f - Mathf.Abs(angleDifference - previousDifference) / 180;

        
        transform.rotation = Quaternion.RotateTowards(rot, targetRotation, delta);
        
        rb.velocity = rb.velocity.magnitude * transform.forward;
        previousDifference = angleDifference;
    }
    
    
    public void Launch(Transform spawnPoint, Transform _target, float launchForce, float _speed)
    {
        target = _target;
        timer = lifeSpan;
        
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
            _hit.GetHit(explosionImpactCurve.Evaluate(t) * explosionPower, transform);
        }

        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        launcher.AfterRocketDie(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
