using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHeightTrigger : MonoBehaviour
{
    public float heightOffset = 200;
    public UnityEvent onHeightEvent;

    private bool triggered;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.playerTransform != null)
        {
            playerTransform = GameManager.Instance.playerTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null) {
            if(!triggered && playerTransform.position.y <= transform.position.y + heightOffset)
            {
                onHeightEvent?.Invoke();
                triggered = true;
            }
        }
    }
}
