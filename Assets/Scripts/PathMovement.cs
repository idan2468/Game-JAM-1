using System;
using BezierSolution;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    
    public BezierSpline path;
    public float speed = 3f;
    public float rotationSpeed = 100f;
    [Range(0f, 1f)] public float startingPoint;
    private float t;
    public float T => t;


    void Start()
    {
        t = startingPoint;
        transform.position = path.GetPoint(t); 
    }

    public void MoveForward()
    {
        if (t >= 1 - Mathf.Epsilon) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(path.GetTangent(t)), Time.deltaTime * rotationSpeed);
        transform.position = path.MoveAlongSpline(ref t, speed * Time.deltaTime);
    }

    public void MoveBackwards()
    {
        if (t <= Mathf.Epsilon) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-path.GetTangent(t)), Time.deltaTime * rotationSpeed);
        transform.position = path.MoveAlongSpline(ref t, - speed * Time.deltaTime);
    }

    public void SetPosition(float _t)
    {
        t = _t;
        transform.position = path.GetPoint(t);
    }
    public void TeleportBack(float distance)
    {
        SetPosition(Mathf.Clamp01(t - distance));
    }
}
