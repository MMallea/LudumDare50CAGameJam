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
    public Image altGroundPosImage;
    public Image altVBar;

    public TextMeshProUGUI timerText;

    [Header("Results UI")]
    public TextMeshProUGUI resultsTimerText;

    [Header("UI Pages")]
    public GameObject gameplayUI;
    public GameObject resultsUI;

    public Transform playerTransform;
    public Transform groundTransform;
    private float altInterval;
    private float playerPosStartY;
    private float playerPosImgStartY;

    // Start is called before the first frame update
    void Start()
    {
        playerPosStartY = playerTransform.position.y;
        playerPosImgStartY = altPlayerPosImage.rectTransform.position.y;
        StartCoroutine(PlayAltPerInterval());
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

                break;
            default:
                break;
        }

        if (timerText != null && GameManager.Instance != null)
            timerText.text = DisplayTimeMinSecMil(GameManager.Instance.GetTimer());

        //Move player image pos closer to ground
        float playerToGroundPerc = (groundTransform.position.y - playerTransform.position.y) / (groundTransform.position.y - playerPosStartY);
        playerToGroundPerc -= 0.025f;
        if (playerToGroundPerc < 0)
            playerToGroundPerc = 0;

        altInterval = playerToGroundPerc;

        float newPlayerPosY = playerPosImgStartY + ((altGroundPosImage.rectTransform.position.y - playerPosImgStartY) * (1 -playerToGroundPerc));

        altPlayerPosImage.rectTransform.position = new Vector3(altPlayerPosImage.rectTransform.position.x, newPlayerPosY, altPlayerPosImage.rectTransform.position.z);
    }

    public void ShowGameplayUI()
    {
        currentUIPage = CurrentUIPage.Gameplay;

        gameplayUI.SetActive(true);
        resultsUI.SetActive(false);
    }

    public void ShowEndGameUI()
    {
        currentUIPage = CurrentUIPage.Results;

        if (resultsTimerText != null && GameManager.Instance != null)
            resultsTimerText.text = DisplayTimeMinSecMil(GameManager.Instance.GetTimer());

        gameplayUI.SetActive(false);
        resultsUI.SetActive(true);
    }

    private IEnumerator PlayAltPerInterval()
    {
        if (GameManager.Instance == null || SoundManager.Instance == null)
            yield break;

        while(GameManager.Instance.IsGameRunning())
        {
            yield return new WaitForSeconds(altInterval * 1.5f);

            SoundManager.Instance.PlaySFX(SoundManager.Instance.altBeep);
        }

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
