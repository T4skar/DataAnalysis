using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    [SerializeField] string phpUrl = "https://citmalumnes.upc.es/~xaviercb12/Sessions.php";

    [SerializeField] int temporalID;

    void OnEnable()
    {
        Simulator.OnNewPlayer += Simulator_OnNewPlayer;
        Simulator.OnNewSession += Simulator_OnNewSession;
        Simulator.OnEndSession += Simulator_OnEndSession;
    }

    void OnDisable()
    {
        Simulator.OnNewPlayer -= Simulator_OnNewPlayer;
        Simulator.OnNewSession -= Simulator_OnNewSession;
        Simulator.OnEndSession -= Simulator_OnEndSession;
    }

    private void Simulator_OnNewPlayer(string playerName, int playerAge, string playerGender, string playerCountry, DateTime signUpTime)
    {
        StartCoroutine(SendPlayerData(playerName, playerAge, playerGender, playerCountry, signUpTime));
    }

    private IEnumerator SendPlayerData(string playerName, int playerAge, string playerGender, string playerCountry, DateTime signUpTime)
    {
        WWWForm form = new WWWForm();

        form.AddField("methodToCall", "CreatePlayer");

        form.AddField("playerName", playerName);
        form.AddField("playerAge", playerAge);
        form.AddField("playerGender", playerGender);
        form.AddField("playerCountry", playerCountry);
        form.AddField("signUpTime", signUpTime.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(phpUrl, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            temporalID = int.Parse(www.downloadHandler.text);
        }
    }

    private void Simulator_OnNewSession(DateTime time)
    {

    }

    private void Simulator_OnEndSession(DateTime time)
    {

    }
}

[System.Serializable]
public class MyData
{
    public string debugMessages;
    public string userId;
}
