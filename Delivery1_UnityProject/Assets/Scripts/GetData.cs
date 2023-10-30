using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    //https://docs.unity3d.com/es/530/Manual/UnityWebRequest.html

    [SerializeField] string phpUrl = "https://citmalumnes.upc.es/~xaviercb12/Sessions.php";

    [SerializeField] string temporalID;

    void OnEnable()
    {
        Simulator.OnNewPlayer += Simulator_OnNewPlayer;
        Simulator.OnNewSession += Simulator_OnNewSession;
        Simulator.OnEndSession += Simulator_OnEndSession;

        //Simulator.GetPlayerID += GetPlayerID;
    }

    //private void GetPlayerID()
    //{
    //    StartCoroutine(ObtainUserID());
    //}

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

        yield return SendToPHP(form);

    }

    private IEnumerator SendToPHP(WWWForm form) 
    {
        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

                string response = www.downloadHandler.text;

                MyData data = JsonUtility.FromJson<MyData>(response);
                Debug.Log(data.userId);
                // if(!Regex.IsMatch(www.downloadHandler.text, @"PHP:")){
                //    temporalID = www.downloadHandler.text;
                //}
                //Debug.Log(temporalID);
            }
        }
    }

    //IEnumerator ObtainUserID()
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("methodToCall", "GetPlayerID");

    //    using (UnityWebRequest www = UnityWebRequest.Get(phpUrl))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            Debug.Log("Error: " + www.error);
    //        }
    //        else
    //        {
    //            string jsonResult = www.downloadHandler.text;
    //            // Analiza el JSON para obtener el valor
    //            var result = JsonUtility.FromJson<int>(jsonResult);
    //            Debug.Log("Valor: " + result);
    //        }
    //    }

    //    yield return null;
    //}

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
