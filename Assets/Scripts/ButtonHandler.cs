using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    private GameObject godObject;
    private GameController gameController;

    void Awake()
    {
        try
        {
            gameController = godObject.GetComponent<GameController>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("everything is fine");
        }

    }

    void Start()
    {
        godObject = GameObject.FindGameObjectWithTag("Controller");
    }

    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
        gameController.isPaused = false;
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameController.isPaused = !gameController.isPaused;
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("ShopMenu");
    }

    public void test()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAA");
    }

    public void NextLevel(int levelNumber)
    {
        string nextLevel = "Level_" + levelNumber;
        SceneManager.LoadScene(nextLevel);
        gameController.isPaused = false;
        Time.timeScale = 1;
    }

    public void Help()
    {
        SceneManager.LoadScene("HelpScreen");
    }
}
