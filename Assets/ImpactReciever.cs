using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ImpactReciever : MonoBehaviour, IDamageable
{
    public float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    
    void Start () {
        character = GetComponent<CharacterController>();
    }
         
    // Update is called once per frame
    void Update () {
        // apply the impact force:
        if (impact.magnitude > 0.2F) character.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }

    public void GetHit(float force, Transform hit)
    {
        Vector3 dir = hit.forward;
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        Debug.DrawLine(transform.position, transform.position + dir*4, Color.blue, 4f);
        impact += dir.normalized * force / mass;
    }
}
