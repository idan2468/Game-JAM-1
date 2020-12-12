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
    public TMP_Text playerWinMessage;
    protected override void Awake()
    {
        playersSliders = new Slider[2];
        playersCooldown = new Image[2];
        playerWinMessage = Resources.Load<GameObject>("Player Won Text").GetComponent<TMP_Text>();
        base.Awake();
    }

    public void UpdatePlayerWon(PlayerIndex p)
    {
        playerWinMessage.SetText(p + " Won!!!");
    }
    
    public void UpdatePlayerCooldownSlider(PlayerIndex p, float val)
    {
        playersSliders[(int) p].value = val;
    }

    public void UpdatePlayerCooldownImage(PlayerIndex p, float val)
    {
        playersCooldown[(int) p].fillAmount = val;
    }
}
