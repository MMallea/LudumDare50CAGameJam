using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

    public enum CurrentUIPage { Gameplay, Results, Menu }
    public CurrentUIPage currentUIPage;

    [Header("Gameplay UI")]
    public Image altPlayerPosImage;
    public TMPro.TextMeshProUGUI altGroundPosText;
    public Image altVBar;

    public TextMeshProUGUI timerText;

    [Header("Results UI")]
    public TextMeshProUGUI resultsTimerText;

    [Header("UI Pages")]
    public GameObject menuUI;
    public GameObject gameplayUI;
    public GameObject resultsUI;

    private float playerPosImgStartY;

    // Start is called before the first frame update
    void Start()
    {
        playerPosImgStartY = altPlayerPosImage.rectTransform.position.y;

        //Start game frozen
        Time.timeScale = 0;
        ShowMenuUI();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentUIPage)
        {
            case CurrentUIPage.Results:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            case CurrentUIPage.Menu:
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Time.timeScale = 1;
                    if (GameManager.Instance != null)
                        GameManager.Instance.StartGame();
                    ShowGameplayUI();
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.RunPlayAltInterval();
                }
                break;
            default:
                break;
        }

        //End game input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (timerText != null && GameManager.Instance != null)
        timerText.text = DisplayTimeMinSecMil(GameManager.Instance.GetTimer());

        //Move player image pos closer to ground;
        float playerToGroundPerc = GameManager.Instance.GetPlayerToGroundPerc() - 0.025f;
        if (playerToGroundPerc < 0)
            playerToGroundPerc = 0;

        float newPlayerPosY = playerPosImgStartY + ((altGroundPosText.rectTransform.position.y - playerPosImgStartY) * (1 -playerToGroundPerc));

        altPlayerPosImage.rectTransform.position = new Vector3(altPlayerPosImage.rectTransform.position.x, newPlayerPosY, altPlayerPosImage.rectTransform.position.z);

    }

    public void ShowMenuUI()
    {
        currentUIPage = CurrentUIPage.Menu;

        menuUI.SetActive(true);
        gameplayUI.SetActive(false);
        resultsUI.SetActive(false);
    }

    public void ShowGameplayUI()
    {
        currentUIPage = CurrentUIPage.Gameplay;

        menuUI.SetActive(false);
        gameplayUI.SetActive(true);
        resultsUI.SetActive(false);
    }

    public void ShowEndGameUI()
    {
        currentUIPage = CurrentUIPage.Results;

        if (resultsTimerText != null && GameManager.Instance != null)
            resultsTimerText.text = DisplayTimeMinSecMil(GameManager.Instance.GetTimer());

        menuUI.SetActive(false);
        gameplayUI.SetActive(false);
        resultsUI.SetActive(true);
    }

    public string DisplayTimeMinSecMil(float time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.Floor(time % 60);
        float fraction = time * 1000;
        fraction = fraction % 1000;

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
    }
}
