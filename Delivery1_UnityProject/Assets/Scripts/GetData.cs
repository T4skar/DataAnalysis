using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    void OnEnable()
    {
        Simulator.OnNewPlayer += Simulator_OnNewPlayer;
        Simulator.OnNewSession += Simulator_OnNewSession;
        Simulator.OnEndSession += Simulator_OnEndSession;

        Simulator.GetPlayerID += GetPlayerID;
    }

    private void GetPlayerID()
    {
        StartCoroutine(ObtainUserID());
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
        string url = "https://citmalumnes.upc.es/~xaviercb12/Sessions.php"; 
        //string url = "www.google.com";
        WWWForm form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("playerAge", playerAge);
        form.AddField("playerGender", playerGender);
        form.AddField("playerCountry", playerCountry);
        form.AddField("signUpTime", signUpTime.ToString("yyyy-MM-dd HH:mm:ss"));

        using (UnityWebRequest www = UnityWebRequest.Post(url,form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    IEnumerator ObtainUserID()
    {
        /*
        using (UnityWebRequest www = UnityWebRequest.Get("URL_de_tu_script_php.php"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                string jsonResult = www.downloadHandler.text;
                // Analiza el JSON para obtener el valor
                var result = JsonUtility.FromJson<MyData>(jsonResult);
                Debug.Log("Valor: " + result.valor);
            }
        }
        */
        yield return null;
    }

    private void Simulator_OnNewSession(DateTime time)
    {

    }

    private void Simulator_OnEndSession(DateTime time)
    {

    }




    //https://docs.unity3d.com/es/530/Manual/UnityWebRequest.html

}
