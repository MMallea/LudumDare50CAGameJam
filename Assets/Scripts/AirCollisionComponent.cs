using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCollisionComponent : MonoBehaviour
{
    public enum CollisionType { Bounce, Slow, Stop };
    public CollisionType collisionType;

    public bool destroyOnImpact;
    public int upForce = 1000;
    public AudioClip collisionSFX;
    public GameObject collisionEffectPrefab;
    private GameObject collisionEffectObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float heightDiff = collision.transform.position.y - transform.position.y;

        //Make sure collision occurs above
        if(heightDiff >= 0 && collision.transform.tag == "Player" && collision.transform.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rBody = collision.transform.GetComponent<Rigidbody>();
            rBody.AddForce(Vector3.up * upForce, ForceMode.Impulse);

            if(SoundManager.Instance != null && collisionSFX != null)
                SoundManager.Instance.PlaySFX(collisionSFX);

            //Instantiate Effect
            if(collisionEffectObj == null)
            {
                if (collisionEffectPrefab != null)
                    collisionEffectObj = Instantiate(collisionEffectPrefab, transform.position, Quaternion.identity, null);
            }
            else
            {
                Debug.Log(collisionEffectObj);
                foreach(ParticleSystem particle in collisionEffectObj.GetComponentsInChildren<ParticleSystem>())
                {
                    particle.Play();
                }
            }

            if(destroyOnImpact)
            {
                destroyOnImpact = false;
                StartCoroutine(DestroyObject());
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
