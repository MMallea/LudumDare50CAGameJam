using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviour
{
    private static ChatManager _instance;

    public static ChatManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ChatManager>();
            }

            return _instance;
        }
    }

    public List<ChatMessage> availableChatMessages;
    private string m_path;
    private IEnumerator avatarLoadCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateData();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        UpdateData();
    }

    public void UpdateData()
    {
        m_path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/discord_output.csv";

        if (File.Exists(m_path))
        {
            try
            {
                byte[] m_bytes = File.ReadAllBytes(m_path);
                string s = System.Text.Encoding.UTF8.GetString(m_bytes);
                availableChatMessages = ReadData(s);
            } catch (IOException)
            {
                Debug.Log("Unable to read file at this time.");
            }
        }
    }

    public ChatMessage GetChatMessage()
    {
        if (availableChatMessages == null)
            return null;

        if(availableChatMessages.Count > 0) {
            ChatMessage messageToGet = availableChatMessages[UnityEngine.Random.Range(0, availableChatMessages.Count)];
            availableChatMessages.Remove(messageToGet);
            return messageToGet;
        }

        return null;
    }

    // Read data from CSV file
    private List<ChatMessage> ReadData(string data)
    {
        Dictionary<string, string> avatarUrls = new Dictionary<string, string>();
        List<ChatMessage> readMessages = new List<ChatMessage>();
        string[] records = data.Split('\n');
        foreach (string record in records)
        {
            string[] fields = record.Split(',');
            if (fields.Length >= 5)
            {
                ChatMessage chatMessage = new ChatMessage(fields[0], fields[1], fields[2], fields[3]);
                readMessages.Add(chatMessage);

                if(!avatarUrls.ContainsKey(fields[0]))
                    avatarUrls.Add(fields[0], fields[4]);
            }
        }

        //Load Avatar images
        //if (avatarLoadCoroutine != null)
        //{
        //    StopCoroutine(avatarLoadCoroutine);
        //    avatarLoadCoroutine = null;
        //}
        //
        //avatarLoadCoroutine = LoadAvatarUrls(readMessages, avatarUrls);
        //if(avatarLoadCoroutine != null)
        //    StartCoroutine(avatarLoadCoroutine);

        return new List<ChatMessage>(readMessages);
    }

    private IEnumerator LoadAvatarUrls(List<ChatMessage> chatMesssages, Dictionary<string, string> avatarUrls)
    {
        foreach (ChatMessage message in chatMesssages)
        {
            if (avatarUrls[message.name] == "[]" || avatarUrls[message.name] == "")
                continue;

            string url = "https://cdn.discordapp.com" + avatarUrls[message.name];
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            } else
            {
                var texture = DownloadHandlerTexture.GetContent(www);
                message.avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                message.onAvatarSet?.Invoke();
            }
        }

        avatarLoadCoroutine = null;
    }
}

public class ChatMessage
{
    public string name;
    public string message;
    public string date;
    public Sprite avatarSprite;
    public string[] reactions;
    public delegate void OnAvatarSet();
    public OnAvatarSet onAvatarSet;

    public ChatMessage(string _name, string _message, string _date, string _reactionListString)
    {
        name = _name;
        message = _message;
        date = _date;
        reactions = _reactionListString.Split(',');
    }
}
