using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour {
    public bool triggerOnce;
    public bool useCollision;
    public Animator anim;
    public List<string> tagsToCheck;

    private bool hasTriggered;
    [SerializeField]
    private bool boolEvent;
    [SerializeField]
    private string eventName;

    [Header("On Event Trigger")]
    public UnityEvent onTrigger;

    private void Start()
    {
        if(anim == null)
            anim = GetComponent<Animator>();
    }

    public void TriggerEvent()
    {
        if ((triggerOnce && hasTriggered) || !this.enabled)
            return;

        if(anim != null)
        {
            if (boolEvent)
                anim.SetBool(eventName, true);
            else
                anim.SetTrigger(eventName);
        }

        onTrigger?.Invoke();

        hasTriggered = true;
    }

    public string GetEventName()
    {
        return eventName;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (tagsToCheck.Count == 0 || tagsToCheck.Contains(coll.tag))
        {
            TriggerEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (anim == null)
            return;

        if (boolEvent && (tagsToCheck.Count == 0 || tagsToCheck.Contains(coll.tag)))
            anim.SetBool(eventName, false);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(useCollision)
        {
            if (tagsToCheck.Count == 0 || tagsToCheck.Contains(coll.gameObject.tag))
            {
                TriggerEvent();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (anim == null)
            return;

        if (useCollision)
        {
            if (boolEvent && (tagsToCheck.Count == 0 || tagsToCheck.Contains(coll.gameObject.tag)))
                anim.SetBool(eventName, false);
        }
    }
}
