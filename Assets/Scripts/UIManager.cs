using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    public bool onGameEndMenu;
    public Image altPlayerPosImage;
    public Image altGroundPosImage;
    public Image altVBar;

    public TMPro.TextMeshProUGUI timerText;

    [Header("UI Pages")]
    public GameObject gameplayUI;
    public GameObject resultsUI;

    private float playerPosImgStartY;

    // Start is called before the first frame update
    void Start()
    {
        playerPosImgStartY = altPlayerPosImage.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerText != null && GameManager.Instance != null)
            timerText.text = DisplayTimeMinSecMil(GameManager.Instance.GetTimer());

        //Move player image pos closer to ground
        //float playerToGroundPerc = player;
        //float newPlayerPosY = playerPosImgStartY + ((playerPosImgStartY - altGroundPosImage.transform.position.y) * playerToGroundPerc);

        //altPlayerPosImage.transform.position = new Vector3(altPlayerPosImage.transform.position.x, newPlayerPosY, altPlayerPosImage.transform.position.z);
    }

    public void ShowGameplayUI()
    {
        onGameEndMenu = false;

        gameplayUI.SetActive(true);
        resultsUI.SetActive(false);
    }

    public void ShowEndGameUI()
    {
        onGameEndMenu = true;

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
