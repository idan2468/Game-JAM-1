using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Explosion : MonoBehaviour, IDamageMaker
{
    public float explosionRadius = 4;
    public float explosionPower = 10;
    public AnimationCurve explosionImpactCurve = AnimationCurve.Linear(0,0,1,1);
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = explosionRadius;
    }
    
    public float MakeDamage(Transform target)
    {
        gameObject.SetActive(false);
        float t = Mathf.Clamp01(Vector3.Distance(transform.position, target.position) / explosionRadius);
        return Mathf.Clamp01(explosionImpactCurve.Evaluate(t)) * (1 + explosionPower/100);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
