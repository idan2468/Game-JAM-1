using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public Slider[] playersSliders;
    public TMP_Text playerWinMessage;
    protected override void Awake()
    {
        playersSliders = new Slider[2];
        playerWinMessage = Resources.Load<GameObject>("Player Won Text").GetComponent<TMP_Text>();
        base.Awake();
    }

    public void UpdatePlayerWon(PlayerIndex p)
    {
        playerWinMessage.SetText(p + " Won!!!");
    }
    
    public void UpdatePlayerCooldownSlider(PlayerIndex p, float val)
    {
        if (playersSliders != null && playersSliders.Length >= (int)p)
        {
            playersSliders[(int) p].value = val;
        }
    }
}
