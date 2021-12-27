using UnityEngine;
using TMPro;

public class PopulateText : MonoBehaviour
{
    private TMP_Text moneyText;

    void Awake()
    {
        moneyText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        moneyText.text = "Money: " + PlayerData.playerMoney;
    }
}
