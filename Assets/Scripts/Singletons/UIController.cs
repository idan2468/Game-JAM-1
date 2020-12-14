using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public Slider[] playersSliders;
    public Image[] playersCooldown;

    public Slider BGMSlider, SFXSlider;

    protected override void Awake()
    {
        playersSliders = new Slider[2];
        playersCooldown = new Image[2];
        
        BGMSlider.value = MusicController.Instance.GetBGMVolume();
        SFXSlider.value = MusicController.Instance.GetSFXVolume();
        
        base.Awake();
    }

    public void RefreshUI()
    {
        BGMSlider.value = MusicController.Instance.GetBGMVolume();
        SFXSlider.value = MusicController.Instance.GetSFXVolume();
    }

    public void UpdatePlayerCooldownSlider(PlayerIndex p, float val)
    {
        playersSliders[(int) p].value = val;
    }

    public void UpdatePlayerCooldownImage(PlayerIndex p, float val)
    {
        var cd = playersCooldown[(int) p];
        cd.fillAmount = val;
    }
}
