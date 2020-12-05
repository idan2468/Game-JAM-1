using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ElevatorController : MonoBehaviour
{
    [SerializeField]private GameObject startPoint;
    [SerializeField]private GameObject endPoint;
    [SerializeField]private float speed = 1.5f;

    private float lastSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = startPoint.transform.position;
        LeanTween.move(gameObject,endPoint.transform.position,2f)
            .setLoopPingPong(-1)
            .setEase(LeanTweenType.easeInOutCubic)
            .setSpeed(speed);
        lastSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed!= lastSpeed)
        {
            LeanTween.cancel(gameObject);
            Start();
        }
    }
}
