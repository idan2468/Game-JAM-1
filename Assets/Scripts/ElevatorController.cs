using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float duration = 2f;
    private Vector3 startPoint, endPoint;
    private float lastSpeed;
    private LTDescr animation;
    
    void Start()
    {
        if (transform.childCount < 2)
        {
            Debug.LogWarning("Object " + name + " is elevator with no points!");
            return;
        }

        startPoint = transform.GetChild(0).position;
        endPoint = transform.GetChild(1).position;
        
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        if (animation != null) LeanTween.cancel(animation.id);

        gameObject.transform.position = startPoint;
        animation = LeanTween.move(gameObject, endPoint, duration).setEaseInOutCubic().setLoopPingPong().setSpeed(speed);
    }
}
