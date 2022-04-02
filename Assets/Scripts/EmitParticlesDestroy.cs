using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticlesDestroy : MonoBehaviour
{
    public int count = 10;
    public float velocityX;

    private float duration;
    private ParticleSystem pSystem;

    public void Start()
    {
        pSystem = GetComponent<ParticleSystem>();

        if (pSystem != null)
        {
            ParticleSystem.VelocityOverLifetimeModule velocityModule = pSystem.velocityOverLifetime;
            duration = pSystem.main.duration + pSystem.main.startLifetime.constant;
            velocityModule.xMultiplier = velocityX;
            StartCoroutine(EmitOverTime());
        }
    }

    public void SetVelocityX(float _velocityX)
    {
        velocityX = _velocityX;
    }

    public IEnumerator EmitOverTime()
    {
        pSystem.Emit(count);

        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
