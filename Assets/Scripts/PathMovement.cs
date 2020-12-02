using System;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public PathManager path;
    public float speed = 3f;
    public int startingIndex;
    private int currentIndex = 0;
    private Rigidbody rb;
    void Start()
    {
        transform.position = startingIndex == -1 ? path.GetPosition(path.points.Length-1) : path.GetPosition(startingIndex);
        transform.LookAt(path.points[1]);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
    
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackwards();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponentInChildren<Animator>().SetTrigger("Jump");
        }
    }

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

    public void MoveForward()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex+1)) < Mathf.Epsilon)
        {
            if (currentIndex+1 >= path.points.Length-1) return;
            currentIndex++;
        }
        transform.position = Vector3.MoveTowards(transform.position, path.GetPosition(currentIndex+1), Time.deltaTime * speed);
        transform.LookAt(path.GetPosition(currentIndex+1));
    }

    public void MoveBackwards()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex)) < Mathf.Epsilon)
        {
            if (currentIndex == 0) return;
            currentIndex--;
        }
        transform.position = Vector3.MoveTowards(transform.position, path.GetPosition(currentIndex), Time.deltaTime * speed);
        transform.LookAt(path.GetPosition(currentIndex));
    }
    
    public void MoveForwardP()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex+1)) < 2)
        {
            if (currentIndex+1 >= path.points.Length-1) return;
            currentIndex++;
        }

        transform.LookAt(path.GetPosition(currentIndex+1));
        rb.AddRelativeForce(speed * Vector3.forward);
    }

    public void MoveBackwardsP()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex)) < 2)
        {
            if (currentIndex == 0) return;
            currentIndex--;
        }
        transform.LookAt(path.GetPosition(currentIndex));
        rb.AddRelativeForce(speed * Vector3.forward);
    }

    public void JumpP()
    {
        
    }
}
