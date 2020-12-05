using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnowballSpawner : MonoBehaviour
{
    public Vector2 delayRange;

    private float timer;
    private Queue<SnowBall> pool;

    private void Awake()
    {
        pool = new Queue<SnowBall>();
        foreach (Transform child in transform)
        {
            SnowBall sb = child.gameObject.GetComponent<SnowBall>();
            if (sb == null) continue;

            sb.spawner = this;
            sb.gameObject.SetActive(false);
            pool.Enqueue(sb);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0) return;

        timer = Random.Range(delayRange.x, delayRange.y);
        Spawn();
    }

    private void Spawn()
    {
        if (pool.Any())
        {
            var sb = pool.Dequeue();
            sb.gameObject.transform.SetParent(null);
            sb.Respawn();
        }
        else
        {
            Debug.LogWarning("No More Snowballs in pool! None was spawned. :(");
        }
    }

    public void AfterSnowballDie(SnowBall sb)
    {
        sb.gameObject.transform.SetParent(gameObject.transform);
        pool.Enqueue(sb);
    }
}
