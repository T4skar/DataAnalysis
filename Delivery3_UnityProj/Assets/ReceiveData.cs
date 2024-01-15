using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#region classes
public class PlayerGetsDamageData
{
    public int id;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public string damageCause;
}

public class EnemyGetsDamageData
{
    public int id;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;
}

public class PlayerDeathData
{
    public int id;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public string deathCause;
}

public class SessionsData
{
    public int id;

    public string start;
    public string end;
}

public class PlayerTrackData
{
    public int id;
    public int session_id;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public float rotX;
    public float rotY;
    public float rotZ;
}

#endregion

public class ReceiveData : MonoBehaviour
{
    [SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xavierlm9/Sessions.php";
    [SerializeField] string playerGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerGetsDmg.php";
    [SerializeField] string enemyGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/EnemyGetsDmg.php";
    [SerializeField] string playerDeathURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerDeath.php";
    [SerializeField] string playerTrackURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerTrack.php";

    public List<PlayerGetsDamageData> PlayerGetsDamageDataList = new();
    public List<EnemyGetsDamageData> EnemyGetsDamageDataList = new();
    public List<PlayerDeathData> PlayerDeathDataList = new();
    public List<SessionsData> SessionsDataList = new();
    public List<PlayerTrackData> PlayerTrackDataList = new();


    public void Start()
    {
        PlayerGetsDamageDataList.Clear();
        EnemyGetsDamageDataList.Clear();
        PlayerDeathDataList.Clear();
        SessionsDataList.Clear();
        PlayerTrackDataList.Clear();

        StartCoroutine(Receive_PlayerGetsDamageData());
        //StartCoroutine(Receive_EnemyGetsDamageData());
        //StartCoroutine(Receive_PlayerDeathData());
        //StartCoroutine(Receive_SessionsData());
        //StartCoroutine(Receive_PlayerTrackData());
    }

    #region receive methods

    public IEnumerator Receive_PlayerGetsDamageData()
    {
        WWWForm form = new();
        form.AddField("methodToCall", "Get Info");

        UnityWebRequest www = UnityWebRequest.Post(playerGetsDmgURL, form);
        
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonData = www.downloadHandler.text;
            // Procesar jsonData según tus necesidades
            //TODO

            PlayerGetsDamageData PlayerGetsDamageData = new();
            PlayerGetsDamageDataList.Add(PlayerGetsDamageData);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
        
    }

    public IEnumerator Receive_EnemyGetsDamageData()
    {
        WWWForm form = new();
        form.AddField("methodToCall", "Get Info");

        UnityWebRequest www = UnityWebRequest.Post(enemyGetsDmgURL, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonData = www.downloadHandler.text;
            // Procesar jsonData según tus necesidades
            //TODO

            EnemyGetsDamageData EnemyGetsDamageData = new();
            EnemyGetsDamageDataList.Add(EnemyGetsDamageData);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public IEnumerator Receive_PlayerDeathData()
    {
        WWWForm form = new();
        form.AddField("methodToCall", "Get Info");

        UnityWebRequest www = UnityWebRequest.Post(playerDeathURL, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonData = www.downloadHandler.text;
            // Procesar jsonData según tus necesidades
            //TODO

            PlayerDeathData PlayerDeathData = new();
            PlayerDeathDataList.Add(PlayerDeathData);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
        
    }

    public IEnumerator Receive_SessionsData()
    {
        WWWForm form = new();
        form.AddField("methodToCall", "Get Info");

        UnityWebRequest www = UnityWebRequest.Post(sessionsUrl, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonData = www.downloadHandler.text;
            // Procesar jsonData según tus necesidades
            //TODO

            SessionsData SessionsData = new();
            SessionsDataList.Add(SessionsData);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
        
    }

    public IEnumerator Receive_PlayerTrackData()
    {
        WWWForm form = new();
        form.AddField("methodToCall", "Get Info");

        UnityWebRequest www = UnityWebRequest.Post(playerTrackURL, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonData = www.downloadHandler.text;
            // Procesar jsonData según tus necesidades
            //TODO

            PlayerTrackData PlayerTrackData = new();
            PlayerTrackDataList.Add(PlayerTrackData);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
        
    }

#endregion

}
