using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveContinuousPlayerDir : MoveContinuous
{
    public void Start()
    {
        base.Start();

        GameObject player = GameObject.Find("PlayerHips");
        if (player != null)
        {
            if (Mathf.Sign(player.transform.position.x - transform.position.x) != Mathf.Sign(velocity.x))
                velocity.x *= -1;

            if (Mathf.Sign(player.transform.position.z - transform.position.z) != Mathf.Sign(velocity.z))
                velocity.z *= -1;
        }
    }
}
