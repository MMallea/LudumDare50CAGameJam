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
                int reactions = chatMessage.reactions.Length;
                if (reactions > 0 && chatMessage.reactions[0] != "[]" && GetComponent<AirCollisionComponent>())
                {
                    AirCollisionComponent airCollisionComponent = GetComponent<AirCollisionComponent>();
                    airCollisionComponent.upForce += Mathf.Clamp(reactions, 0, 20) * 50;
                    airCollisionComponent.onCollided += () =>
                    {
                        if (GameManager.Instance.playerParticles != null)
                        {
                            GameManager.Instance.playerParticles.Emit(reactions);
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
