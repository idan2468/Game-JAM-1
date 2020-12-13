using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{
    public static Transform camTransform;

    private void Awake()
    {
        if (camTransform == null)
            camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }
}