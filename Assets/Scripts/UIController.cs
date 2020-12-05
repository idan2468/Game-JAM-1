using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance = null;
    [SerializeField] private const float HEART_ANIMATION_TIME = 1f;
    [SerializeField] private const float WAIT_BETWEEN_ANIMATION = HEART_ANIMATION_TIME;
    private const int TOTAL_NUN_HEARTS = 3;
    [SerializeField] Image[] playerOneLife;
    [SerializeField] Image[] playerTwoLife;
    private float loosenHeartsPlayer1 = 0;
    private float loosenHeartsPlayer2 = 0;
    // Start is called before the first frame update

    public enum PlayerIndex
    {
        Player1 = 1,
        Player2 = 2
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public static UIController getInstance()
    {
        return instance;
    }

    void Start()
    {
        StartCoroutine(sample());
    }
    public IEnumerator sample()
    {
        yield return UpdateDamageGUI(PlayerIndex.Player1, 2.5f);
        Debug.Log("LoosenHearts " + loosenHeartsPlayer1.ToString());
        yield return UpdateDamageGUI(PlayerIndex.Player2, 1f);
        Debug.Log("LoosenHearts " + loosenHeartsPlayer2.ToString());
        yield return UpdateHealGUI(PlayerIndex.Player1, 2.5f);
        Debug.Log("LoosenHearts " + loosenHeartsPlayer1.ToString());
        yield return UpdateHealGUI(PlayerIndex.Player2, 1f);
        Debug.Log("LoosenHearts " + loosenHeartsPlayer2.ToString());
    }
    public IEnumerator UpdateDamageGUI(PlayerIndex playerIndex, float damage)
    {
        var loosenHearts = playerIndex == PlayerIndex.Player1 ? loosenHeartsPlayer1 : loosenHeartsPlayer2;
        if (damage < 0 || loosenHearts + damage > TOTAL_NUN_HEARTS)
        {
            yield break;
        }
        var images = playerIndex == PlayerIndex.Player1 ? playerOneLife : playerTwoLife;
        int currHeartIndex = Mathf.FloorToInt(loosenHearts);
        while (damage > 0)
        {
            float currDamage = damage > .5f && images[currHeartIndex].fillAmount == 1 ? 1f : .5f;
            float start = images[currHeartIndex].fillAmount;
            float end = start - currDamage;
            UpdateFillOfLifeHeartByIndex(currHeartIndex, playerIndex, start, end);
            yield return new WaitForSeconds(WAIT_BETWEEN_ANIMATION);
            damage -= currDamage;
            loosenHearts += currDamage;
            currHeartIndex = end == 0 ? currHeartIndex + 1 : currHeartIndex;
        }
        // Update loosen heart by player
        if (playerIndex == PlayerIndex.Player1)
        {
            loosenHeartsPlayer1 = loosenHearts;
        }
        else
        {
            loosenHeartsPlayer2 = loosenHearts;
        }
    }

    public IEnumerator UpdateHealGUI(PlayerIndex playerIndex, float heal)
    {
        var loosenHearts = playerIndex == PlayerIndex.Player1 ? loosenHeartsPlayer1 : loosenHeartsPlayer2;
        if(heal < 0 || loosenHearts - heal < 0)
        {
            yield break;
        }
        var images = playerIndex == PlayerIndex.Player1 ? playerOneLife : playerTwoLife;
        int currHeartIndex = Mathf.CeilToInt(loosenHearts) - 1;
        while (heal > 0)
        {
            float start = images[currHeartIndex].fillAmount;
            float currHeal = heal > .5f && start == 0 ? 1f : .5f;
            float end = start + currHeal;
            UpdateFillOfLifeHeartByIndex(currHeartIndex, playerIndex, start, end);
            yield return new WaitForSeconds(WAIT_BETWEEN_ANIMATION);
            heal -= currHeal;
            loosenHearts -= currHeal;
            currHeartIndex = end == 1 ? currHeartIndex - 1 : currHeartIndex;
        }
        // Update loosen heart by player
        if (playerIndex == PlayerIndex.Player1)
        {
            loosenHeartsPlayer1 = loosenHearts;
        }
        else
        {
            loosenHeartsPlayer2 = loosenHearts;
        }
    }


    private void UpdateFillOfLifeHeartByIndex(int heartIndex, PlayerIndex playerIndex, float start, float end)
    {
        var heartImages = playerIndex == PlayerIndex.Player1 ? playerOneLife : playerTwoLife;
        LeanTween.value(gameObject, start, end, HEART_ANIMATION_TIME).setOnUpdate((float val) =>
        {
            heartImages[heartIndex].fillAmount = val;

        }).setEaseOutQuad();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
