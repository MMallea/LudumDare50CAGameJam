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
