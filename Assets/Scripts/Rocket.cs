﻿using System.Collections.Generic;
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

    private float speed;
    private Transform target;
    [HideInInspector] public RocketLauncher launcher;
    private Rigidbody rb;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) Explode();
    }

    void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        // transform.rotation = Quaternion.Slerp(transform.rotation,
        // Quaternion.AngleAxis(Mathf.Acos(Vector3.Dot(Vector3.forward, direction)) * Mathf.Rad2Deg, Vector3.Cross(Vector3.forward, direction)),
        // Time.fixedDeltaTime * rotationSpeed);
        var targetRotation = Quaternion.LookRotation(direction );
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        rb.velocity = Mathf.Lerp(rb.velocity.magnitude, speed, Time.fixedDeltaTime) * transform.forward;
        // rb.velocity = rb.velocity.magnitude * transform.forward;
    }

    public void Launch(Transform spawnPoint, Transform _target, float launchForce, float _speed)
    {
        speed = _speed;
        target = _target;
        timer = lifeSpan;
        rb.velocity = Vector3.zero;
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
