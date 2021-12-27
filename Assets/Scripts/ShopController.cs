using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Sprite buttonPressed;
    [SerializeField] private TMP_Text moneyText;

    private Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void BuyItem(int type)
    {
        if (type == 1)
        {
            if (!PlayerData.ownsIceBlast)
            {
                if (PlayerData.playerMoney >= 50)
                {
                    PlayerData.playerMoney -= 50;
                    PlayerData.ownsIceBlast = true;
                    SetImage();
                }
                else
                {
                    Debug.Log("not enough money!");
                    StartCoroutine("TextFlash");
                }
            }
            else
            {
                Debug.Log("you already own this!");
            }
        }
        else if (type == 2)
        {
            if (!PlayerData.ownsDrone)
            {
                if (PlayerData.playerMoney >= 10)
                {
                    PlayerData.playerMoney -= 10;
                    PlayerData.ownsDrone = true;
                    SetImage();
                }
                else
                {
                    Debug.Log("not enough money!");
                    StartCoroutine("TextFlash");
                }
            }
            else
            {
                Debug.Log("you already own this!");
            }
        }
        else if (type == 3)
        {
            if (!PlayerData.ownsRocket)
            {
                if (PlayerData.playerMoney >= 1000)
                {
                    PlayerData.playerMoney -= 1000;
                    PlayerData.ownsRocket = true;
                    SetImage();
                }
                else
                {
                    Debug.Log("not enough money!");
                    StartCoroutine("TextFlash");
                }
            }
            else
            {
                Debug.Log("you already own this!");
            }
        }
        else if (type == 4)
        {
            if (!PlayerData.ownsBuddy)
            {
                if (PlayerData.playerMoney >= 100)
                {
                    PlayerData.playerMoney -= 100;
                    PlayerData.ownsBuddy = true;
                    SetImage();
                }
                else
                {
                    Debug.Log("not enough money!");
                    StartCoroutine("TextFlash");
                }
            }
            else
            {
                Debug.Log("you already own this!");
            }
        }
    }
    public void SetImage()
    {
        image.sprite = buttonPressed;
        button.interactable = false;
    }

    private IEnumerator TextFlash()
    {
        for (int j = 0; j < 5; j++)
        {
            moneyText.color = Color.red;
            yield return new WaitForSeconds(.1f);
            moneyText.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }
}
