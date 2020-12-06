using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBasic : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private Camera cam;
    [SerializeField] private float rotationSpeed = 2;
    private void Start()
    {
        
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Debug Vertical");
        var horizontal = Input.GetAxis("Debug Horizontal");
        var dir = new Vector3(horizontal, 0, vertical);
        if (dir.magnitude > .1)
        {
            var rotationDir = MoveToDir(dir);
            // RotateTowardsMove(rotationDir);
        }
    }

    private void RotateTowardsMove(Vector3 dir)
    {
        var rotation = Quaternion.LookRotation(dir);
        // LeanTween.rotate(gameObject, rotation.eulerAngles, 0.1f).setEase(LeanTweenType.easeInOutCubic);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,rotationSpeed);
    }

    

    private Vector3 MoveToDir(Vector3 dir)
    {
        var forwardAccordingToCamera = Quaternion.Euler(0f, cam.gameObject.transform.eulerAngles.y, 0f) * dir;
        var targetPos = transform.position + forwardAccordingToCamera * (speed * Time.deltaTime);
        transform.position = targetPos;
        return forwardAccordingToCamera;
    }
    
    
}
