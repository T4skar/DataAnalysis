using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
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

    private void Simulator_OnNewPlayer(string playerName, string playerCountry, DateTime time)
    {
        //string[] a = { playerName , playerCountry , };
        //StartCoroutine(SendPlayerData(playerName, playerCountry, time));
        StartCoroutine(SendPlayerData(playerName, playerCountry, time));
    }
    
    private IEnumerator SendPlayerData(string playerName, string playerCountry, DateTime time)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myapi", "{ \"PlayerName\": " + playerName + ", \"PlayerCountry\":" + playerCountry + ", \"PlayerCountry\":" + time.ToString() + " }", "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    private void Simulator_OnNewSession(DateTime time)
    {

    }

    private void Simulator_OnEndSession(DateTime time)
    {

    }




    //https://docs.unity3d.com/es/530/Manual/UnityWebRequest.html

}
