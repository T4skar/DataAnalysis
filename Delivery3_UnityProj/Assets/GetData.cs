using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour, IMessageReceiver
{
    // === Variables ===

    [SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xaviercb12/Sessions.php";
    [SerializeField] string usersUrl = "https://citmalumnes.upc.es/~xaviercb12/Users.php";
    [SerializeField] string purchasesUrl = "https://citmalumnes.upc.es/~xaviercb12/Purchases.php";

    //[SerializeField] int temporalUserID = -1;
    //[SerializeField] int temporalSessionID = -1;

    void OnEnable()
    {
        Simulator.OnNewPlayer += Simulator_OnNewPlayer;
        Simulator.OnNewSession += Simulator_OnNewSession;
        Simulator.OnEndSession += Simulator_OnEndSession;
        Simulator.OnBuyItem += Simulator_OnItemBuy;
    }

    void OnDisable()
    {
        Simulator.OnNewPlayer -= Simulator_OnNewPlayer;
        Simulator.OnNewSession -= Simulator_OnNewSession;
        Simulator.OnEndSession -= Simulator_OnEndSession;
        Simulator.OnBuyItem -= Simulator_OnItemBuy;
    }

    // === Coroutines ===

    private void Simulator_OnNewPlayer(string playerName, int playerAge, string playerGender, string playerCountry, DateTime signUpTime)
    {
        StartCoroutine(SendPlayerData(playerName, playerAge, playerGender, playerCountry, signUpTime, usersUrl));
    }

    private void Simulator_OnNewSession(int userId, DateTime startTime)
    {
        StartCoroutine(SendSessionData(userId, startTime, true, sessionsUrl));
    }

    private void Simulator_OnEndSession(DateTime endTime, int sessionId, int userId)
    {
        StartCoroutine(SendSessionData(userId, endTime, false, sessionsUrl, sessionId));
    }

    private void Simulator_OnItemBuy(int userId, int moneySpent, DateTime time, int sessionId)
    {
        StartCoroutine(SendPurchaseData(time, userId, moneySpent, purchasesUrl, sessionId));
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
            CallbackEvents.OnAddPlayerCallback.Invoke(temporalUserID);
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
                CallbackEvents.OnNewSessionCallback.Invoke(userId, sessionId);
            }
        }
        else
        {
            if (sessionId != -1)
            {
                CallbackEvents.OnEndSessionCallback.Invoke(userId);
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

        CallbackEvents.OnItemBuyCallback.Invoke(sessionId, userId);
    }
}