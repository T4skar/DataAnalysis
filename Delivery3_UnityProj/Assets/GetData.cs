using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour, IMessageReceiver
{
    // === Variables ===

    [SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xavierlm9/Sessions.php";
    [SerializeField] string playerGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerGetsDmg.php";
    [SerializeField] string enemyGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/EnemyGetsDmg.php";
    [SerializeField] string playerTrackURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerTrack.php";

    //[SerializeField] int temporalUserID = -1;
    //[SerializeField] int temporalSessionID = -1;

    [SerializeField]
    Damageable Ellen;

    [SerializeField]
    List<Damageable> enemies = new();

    void OnEnable()
    {
        Ellen.onDamageMessageReceivers.Add(this);

        foreach (Damageable damageable in enemies)
        {
            damageable.onDamageMessageReceivers.Add(this);
        }
    }

    void OnDisable()
    {
    }

    private void Start()
    {
        
    }

    // === La chicha (IEnumerators) ===

    private IEnumerator SendPlayerData(string playerName, int playerAge, string playerGender, string playerCountry, DateTime signUpTime, string url)
    {
        WWWForm form = new WWWForm();

        form.AddField("methodToCall", "CreatePlayer");

        form.AddField("playerName", playerName);
        form.AddField("playerAge", playerAge);
        form.AddField("playerGender", playerGender);
        form.AddField("playerCountry", playerCountry);
        form.AddField("signUpTime", signUpTime.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        int temporalUserID;

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            temporalUserID = -1;
        }
        else
        {
            Debug.Log("Form upload to " + url + " complete!");
            Debug.Log(www.downloadHandler.text);
            temporalUserID = int.Parse(www.downloadHandler.text);
        }

        if (temporalUserID != -1)
        {
            //CallbackEvents.OnAddPlayerCallback.Invoke(temporalUserID);
        }
    }

    private IEnumerator SendSessionData(int userId, DateTime date, bool startSession, string url, int sessionId = -1)
    {
        WWWForm form = new WWWForm();

        form.AddField("sessionId", sessionId);
        form.AddField("start", startSession.ToString());
        form.AddField("userId", userId);
        form.AddField("timeStamp", date.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            sessionId = -1;
        }
        else
        {
            Debug.Log("Form upload to " + url + " complete!");
            if (startSession)
            {
                Debug.Log("Session started: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Session ended: " + www.downloadHandler.text);
            }
            sessionId = int.Parse(www.downloadHandler.text);
        }

        if (startSession)
        {
            if (sessionId != -1)
            {
                //CallbackEvents.OnNewSessionCallback.Invoke(userId, sessionId);
            }
        }
        else
        {
            if (sessionId != -1)
            {
                //CallbackEvents.OnEndSessionCallback.Invoke(userId);
            }
        }

    }

    private IEnumerator SendPurchaseData(DateTime date, int userId, int itemId, string url, int sessionId)
    {
        WWWForm form = new WWWForm();

        form.AddField("itemId", itemId);
        form.AddField("userId", userId);
        form.AddField("timeStamp", date.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Form upload to " + url + " complete!");
            if (www.downloadHandler.text != null)
            {
                Debug.Log("Item ID: " + www.downloadHandler.text);
            }
        }

        //CallbackEvents.OnItemBuyCallback.Invoke(sessionId, userId);
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        Debug.Log("Message recieved");

        switch (type)
        {
            case MessageType.DAMAGED:
                Send_PlayerGetsDamage();
                break;

            case MessageType.DEAD:
                Send_PlayerDeath();
                break;

            case MessageType.RESPAWN:

                break;
        }
    }

    public void Send_PlayerGetsDamage()
    {

    }

    public void Send_EnemyGetsDamage()
    {

    }

    public void Send_PlayerDeath()
    {

    }

    public void Send_Sessions()
    {

    }

    public void Send_PlayerTrack()
    {

    }
}