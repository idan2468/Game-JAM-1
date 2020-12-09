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
        playersSliders[(int) p].value = val;
    }
}
