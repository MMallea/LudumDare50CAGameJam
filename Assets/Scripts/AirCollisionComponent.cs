using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirCollisionComponent : MonoBehaviour
{
    public enum CollisionType { Bounce, Slow, Stop };
    public CollisionType collisionType;

    public bool destroyOnImpact;
    public int upForce = 1000;
    public AudioClip collisionSFX;
    public AudioClip painSFX;
    public Animator anim;
    public bool scaleCollisionEffectWithObj;
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
            {
                SoundManager.Instance.PlaySFX(collisionSFX);
                SoundManager.Instance.PlaySFX(painSFX);
            }

            //Instantiate Effect
            if(collisionEffectObj == null)
            {
                if (collisionEffectPrefab != null)
                    collisionEffectObj = Instantiate(collisionEffectPrefab, transform.position, Quaternion.identity, null);
            }
            else
            {
                if(scaleCollisionEffectWithObj)
                    collisionEffectObj.transform.localScale = transform.localScale;

                foreach(ParticleSystem particle in collisionEffectObj.GetComponentsInChildren<ParticleSystem>())
                {
                    particle.Play();
                }
            }

            if(destroyOnImpact)
            {
                destroyOnImpact = false;
                StartCoroutine(Despawn());
            }
            if (GameManager.Instance != null)
                GameManager.Instance.SetRumble(0.25f, 0.25f);
        }
    }

    private IEnumerator Despawn()
    {
        float delay = 0.1f;
        if(anim != null)
        {
            anim.SetTrigger("Hit");
            yield return null;
            yield return null;
            delay = anim.GetCurrentAnimatorStateInfo(0).length;
        }

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
