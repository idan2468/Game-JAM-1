using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureParticle : MonoBehaviour
{
    private ParticleSystem particle;
    private bool isPlaying;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayParticle()
    {
        particle.Play();
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying) particle.Simulate(Time.unscaledDeltaTime, true, false);
    }
}
