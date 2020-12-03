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
    void Start()
    {
        t = startingPoint;
        transform.position = path.GetPoint(t); 
    }

    public void MoveForward()
    {
        if (t >= 1 - Mathf.Epsilon) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(path.GetTangent(t)), Time.deltaTime * rotationSpeed);
        // transform.rotation = Quaternion.LookRotation(path.GetTangent(t));
        transform.position = path.MoveAlongSpline(ref t, speed * Time.deltaTime);
    }

    public void MoveBackwards()
    {
        if (t <= Mathf.Epsilon) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-path.GetTangent(t)), Time.deltaTime * rotationSpeed);
        // transform.rotation = Quaternion.LookRotation(-path.GetTangent(t));
        transform.position = path.MoveAlongSpline(ref t, - speed * Time.deltaTime);
    }

    public void TeleportBack(float distance)
    {
        t = Mathf.Clamp01(t - distance);
        transform.position = path.GetPoint(t);
    }
    
    #region Physics Section (should be activated if we decide to do in physics)
    // private void FixedUpdate()
    // {
    //     if (Input.GetKey(KeyCode.UpArrow))
    //     {
    //         MoveForwardP();
    //     }
    //     
    //     if (Input.GetKey(KeyCode.DownArrow))
    //     {
    //         MoveBackwardsP();
    //     }
    // }
    
    // public void MoveForwardP()
    // {
    //     if (Vector3.Distance(transform.position, path.GetPosition(currentIndex+1)) < 2)
    //     {
    //         if (currentIndex+1 >= path.points.Length-1) return;
    //         currentIndex++;
    //     }
    //
    //     transform.LookAt(path.GetPosition(currentIndex+1));
    //     rb.AddRelativeForce(speed * Vector3.forward);
    // }
    //
    // public void MoveBackwardsP()
    // {
    //     if (Vector3.Distance(transform.position, path.GetPosition(currentIndex)) < 2)
    //     {
    //         if (currentIndex == 0) return;
    //         currentIndex--;
    //     }
    //     transform.LookAt(path.GetPosition(currentIndex));
    //     rb.AddRelativeForce(speed * Vector3.forward);
    // }
    #endregion
}
