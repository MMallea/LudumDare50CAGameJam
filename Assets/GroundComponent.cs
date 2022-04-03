using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComponent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerCollideWithGround(Collision collision)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.EndGame();

        if (UIManager.Instance != null)
            UIManager.Instance.ShowEndGameUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
            OnPlayerCollideWithGround(collision);
    }
}
