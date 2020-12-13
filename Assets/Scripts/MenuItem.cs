﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    private int originalSort;
    private Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        if (canvas != null) originalSort = canvas.sortingOrder;
    }

    public void SetInFront()
    {
        canvas.sortingOrder = 20;
    }

    public void UnsetInFront()
    {
        canvas.sortingOrder = originalSort;
    }

    public void PlayClick()
    {
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Click);
    }

    public void PlayHover()
    {
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Hover);
    }

    public void ChangeVolume(Slider slider)
    {
        MusicController.Instance.ChangeSoundEffectsVolume(slider);
    }
}
