using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsPlayer : MonoBehaviour
{
    private float stepVolume = .5f;

    /**
     * 1≤i≤4
     */
    public void PlayStep(int i)
    {
        MusicController.Instance.PlaySound("Ground_"+i, stepVolume);
    }
}
