using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private const int HEART_ANIMATION_TIME = 1;
    [SerializeField] Image[] playerOneLife;
    [SerializeField] Image[] playerTwoLife;
    private float playerOneCurrLife = 3f;
    private float playerTwoCurrLife = 3f;
    // Start is called before the first frame update
    void next()
    {

    }

    void Start()
    {
        removeHalfLife(0);       
    }

   private void removeHalfLife(int heartIndex)
    {
        LeanTween.value(gameObject, 1f, 0.5f, HEART_ANIMATION_TIME).setOnUpdate((float val) => {
            playerOneLife[heartIndex].fillAmount = val;
            playerTwoLife[heartIndex].fillAmount = val;

        }).setEaseOutQuad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
