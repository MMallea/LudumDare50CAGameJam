using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessageScript : MonoBehaviour
{
    public SpriteRenderer profileSprite;
    public TMP_Text nameText;
    public TMP_Text dateText;
    public TMP_Text messageText;
    public AudioClip emojiSFX;

    [Header("Chat Bubble References")]
    public Transform bubbleTransform;
    public BoxCollider bubbleCollider;
    public Transform chatContentTransfom;
    private ChatMessage chatMessage;

    // Start is called before the first frame update
    void Start()
    {
        if (ChatManager.Instance != null)
        {
            chatMessage = ChatManager.Instance.GetChatMessage();

            if(chatMessage != null)
            {
                nameText.text = chatMessage.name;
                dateText.text = chatMessage.date;
                messageText.text = chatMessage.message;
                chatMessage.onAvatarSet = SetProfileImage;

                //Update bounce with emojis
                int reactionsAmnt = chatMessage.reactions.Length;
                if (reactionsAmnt > 0 && chatMessage.reactions[0] != "[]" && GetComponent<AirCollisionComponent>())
                {
                    int maxReactionMultiplier = Mathf.Clamp(reactionsAmnt, 0, 15);
                    AirCollisionComponent airCollisionComponent = GetComponent<AirCollisionComponent>();
                    //Update upforce
                    airCollisionComponent.upForce += maxReactionMultiplier * 25;
                    airCollisionComponent.collisionSFX = emojiSFX;

                    //Make sure size is equal or greater to emoji max
                    float minSize = (float)maxReactionMultiplier * 0.5f;
                    if (gameObject.transform.localScale.x < minSize)
                        gameObject.transform.localScale = Vector3.one * minSize;

                    airCollisionComponent.onCollided += () =>
                    {
                        if (GameManager.Instance.playerParticles != null)
                        {
                            GameManager.Instance.playerParticles.Emit(reactionsAmnt);
                        }
                    };
                }

            } else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetProfileImage()
    {
        if(chatMessage != null)
        {
            if (profileSprite != null) profileSprite.sprite = chatMessage.avatarSprite;
        }
    }
}
