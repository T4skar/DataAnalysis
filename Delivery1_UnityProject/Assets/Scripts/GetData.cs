using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    // === Variables ===

    [SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xaviercb12/Sessions.php";
    [SerializeField] string usersUrl = "https://citmalumnes.upc.es/~xaviercb12/Users.php";
    [SerializeField] string purchasesUrl = "https://citmalumnes.upc.es/~xaviercb12/Purchases.php";

    [SerializeField] int temporalUserID = 0;
    [SerializeField] int temporalSessionID = 0;

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
        //CallbackEvents.OnAddPlayerCallback.Invoke(0);
    }

    private void Simulator_OnNewSession(DateTime time)
    {
        StartCoroutine(SendSessionData(time, true, sessionsUrl));
        CallbackEvents.OnNewSessionCallback.Invoke(0);
    }

    private void Simulator_OnEndSession(DateTime time)
    {
        StartCoroutine(SendSessionData(time, false, sessionsUrl));
        CallbackEvents.OnEndSessionCallback.Invoke(0);
    }

    private void Simulator_OnItemBuy(int id, DateTime time)
    {
        StartCoroutine(SendPurchaseData(time, id, purchasesUrl));
        CallbackEvents.OnItemBuyCallback.Invoke();
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

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Form upload to " + url + " complete!");
            Debug.Log(www.downloadHandler.text);
            temporalUserID = int.Parse(www.downloadHandler.text);
        }
    }

    private IEnumerator SendSessionData(DateTime date, bool startSession, string url)
    {
        int id;

        if (startSession)
        {
            id = temporalSessionID++;
        }
        else
        {
            id = temporalSessionID;
        }

        WWWForm form = new WWWForm();

        form.AddField("sessionId", id);
        form.AddField("start", startSession.ToString());
        form.AddField("userId", temporalUserID);
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
            Debug.Log(www.downloadHandler.text);
            temporalSessionID = int.Parse(www.downloadHandler.text);
        }
    }

    private IEnumerator SendPurchaseData(DateTime date, int purchaseId, string url)
    {
        WWWForm form = new WWWForm();

        form.AddField("purchaseId", purchaseId);
        form.AddField("userId", temporalUserID);
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
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}