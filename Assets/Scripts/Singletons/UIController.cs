using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public Slider[] playersSliders;

    public void UpdatePlayerCooldownSlider(PlayerIndex p, float val)
    {
        if (playersSliders != null && playersSliders.Length >= (int)p)
        {
            playersSliders[(int) p].value = val;
        }
    }
}
