using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform target;
    public float launchForce = 500f;
    public float stabilizeRocketSpeed = 3f;

    private Queue<Rocket> rocketPool;

    void Awake()
    {
        rocketPool = new Queue<Rocket>();
        foreach (Transform child in transform)
        {
            Rocket rocket = child.gameObject.GetComponent<Rocket>();
            if (rocket == null) continue;

            rocket.launcher = this;
            rocketPool.Enqueue(rocket);
        }
    }

    public void Launch()
    {
        if (rocketPool.Any())
        {
            var rocket = rocketPool.Dequeue();
            rocket.gameObject.transform.SetParent(null);
            rocket.Launch(transform, target, launchForce, stabilizeRocketSpeed);
        }
        else
        {
            Debug.LogWarning("No More rockets in pool! None was launched. :(");
        }
    }

    public void AfterRocketDie(Rocket rocket)
    {
        rocket.gameObject.transform.SetParent(gameObject.transform);
        rocketPool.Enqueue(rocket);
    }

}
