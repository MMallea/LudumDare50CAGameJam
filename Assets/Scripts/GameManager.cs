using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Text;

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
    public ParticleSystem playerParticles;
    public GameObject characterWrapperObj;
    public TextMeshProUGUI highscore;

    private bool gameRunning;
    private float playerStartHeight = 0;
    private float rumbleTime;
    private float rumbleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform != null)
            playerStartHeight = playerTransform.position.y;
        highscore.text = "Highscore: " + UIManager.DisplayTimeMinSecMil(PlayerPrefs.GetFloat("Highscore", 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
        }

        if(rumbleTime > 0)
        {
            if(Gamepad.current != null)
                Gamepad.current.SetMotorSpeeds(rumbleSpeed, rumbleSpeed);

            rumbleTime -= Time.unscaledDeltaTime;
            if (Gamepad.current != null && rumbleTime <= 0)
                Gamepad.current.SetMotorSpeeds(0, 0);
        }

    }


    public void StartGame()
    {
        timer = 0;
        gameRunning = true;
        if (characterWrapperObj != null)
            characterWrapperObj.SetActive(true);

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SoundManager.Instance.scream);
    }
    public void EndGame()
    {
        gameRunning = false;
        if (timer > PlayerPrefs.GetFloat("Highscore", 0f))
            PlayerPrefs.SetFloat("Highscore", timer);

        Time.timeScale = 0;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.groundHit);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.death);
            SoundManager.Instance.windSource.Stop();
        }

        SetRumble(1f, 1f);
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

    public float GetPlayerToGroundPerc()
    {
        return (groundTransform.position.y - playerTransform.position.y) / (groundTransform.position.y - playerStartHeight);
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void SetRumble(float time, float speed)
    {
        rumbleTime = time;
        rumbleSpeed = speed;
    }

    public bool IsRumbling()
    {
        return rumbleTime > 0;
    }
}
