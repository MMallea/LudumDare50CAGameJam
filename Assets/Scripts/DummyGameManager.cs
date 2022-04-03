using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGameManager : MonoBehaviour
{
    private static DummyGameManager _instance;

    public static DummyGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DummyGameManager>();
            }

            return _instance;
        }
    }

    public float timer = 0;

    public Transform playerTransform;
    public Transform groundTransform;
    private float playerStartHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform != null)
            playerStartHeight = playerTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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
