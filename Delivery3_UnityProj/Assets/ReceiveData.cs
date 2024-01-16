using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#region classes
public class PlayerGetsDamageData
{
    public PlayerGetsDamageData()
    {

    }

    public int id_PlayerGetsDamage;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public string damageCause;
}

public class EnemyGetsDamageData
{
    public EnemyGetsDamageData()
    {

    }

    public int id_EnemyGetsDamage;
    public string Timestamp;

    public float PosX;
    public float PosY;
    public float PosZ;
}

public class PlayerDeathData
{
    public PlayerDeathData()
    {

    }

    public int id_PlayerDeath;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public string deathCause;
}

public class SessionsData
{
    public SessionsData()
    {

    }

    public int Session_Id;
    public int User_Id;

    public string Start;
    public string End;
}

public class PlayerTrackData
{
    public PlayerTrackData()
    {

    }

    public int id_PlayerTrack;
    public int Session_Id;
    public string Timestamp;

    public float Pos_X;
    public float Pos_Y;
    public float Pos_Z;

    public float Rot_X;
    public float Rot_Y;
    public float Rot_Z;
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

        //Enable functions only when the tables are not empty
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

            // Dividir las filas del JSON por salto de línea
            string[] rows = jsonData.Split('\n');

            // Procesar cada fila individualmente
            foreach (string row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // Deserializar la fila actual
                    PlayerGetsDamageData dataObject = JsonUtility.FromJson<PlayerGetsDamageData>(row);
                    PlayerGetsDamageDataList.Add(dataObject);
                }
            }
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

            // Dividir las filas del JSON por salto de línea
            string[] rows = jsonData.Split('\n');

            // Procesar cada fila individualmente
            foreach (string row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // Deserializar la fila actual
                    EnemyGetsDamageData dataObject = JsonUtility.FromJson<EnemyGetsDamageData>(row);
                    EnemyGetsDamageDataList.Add(dataObject);
                }
            }
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

            // Dividir las filas del JSON por salto de línea
            string[] rows = jsonData.Split('\n');

            // Procesar cada fila individualmente
            foreach (string row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // Deserializar la fila actual
                    PlayerDeathData dataObject = JsonUtility.FromJson<PlayerDeathData>(row);
                    PlayerDeathDataList.Add(dataObject);
                }
            }
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

            // Dividir las filas del JSON por salto de línea
            string[] rows = jsonData.Split('\n');

            // Procesar cada fila individualmente
            foreach (string row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // Deserializar la fila actual
                    SessionsData dataObject = JsonUtility.FromJson<SessionsData>(row);
                    SessionsDataList.Add(dataObject);
                }
            }
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

            // Dividir las filas del JSON por salto de línea
            string[] rows = jsonData.Split('\n');

            // Procesar cada fila individualmente
            foreach (string row in rows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // Deserializar la fila actual
                    PlayerTrackData dataObject = JsonUtility.FromJson<PlayerTrackData>(row);
                    PlayerTrackDataList.Add(dataObject);
                }
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

#endregion

}
