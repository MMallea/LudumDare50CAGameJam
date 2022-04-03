using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public float timer = 0;
    public Transform playerTransform;
    public Transform groundTransform;

    private bool gameRunning;
    private float playerStartHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform != null)
            playerStartHeight = playerTransform.position.y;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunning)
        {
            timer += Time.deltaTime;
        }
    }


    public void StartGame()
    {
        timer = 0;
        gameRunning = true;
    }
    public void EndGame()
    {
        gameRunning = false;
        Time.timeScale = 0;
    }

    public float GetPlayerHeight()
    {
        if (playerTransform != null)
            return playerTransform.position.y;

            return 0;
    }

    public float GetPlayerStartHeight()
    {
        return playerStartHeight;
    }

    public float GetGroundHeight()
    {
        if (groundTransform != null)
            return groundTransform.position.y;

        return 0;
    }

    public float GetTimer()
    {
        return timer;
    }
}