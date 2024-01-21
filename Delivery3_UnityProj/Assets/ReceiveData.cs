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
    public int user_id;
    public string timeStamp;

    public float posX;
    public float posY;
    public float posZ;

    public string damageCause;
}

public class EnemyDeathData
{
    public EnemyDeathData()
    {

    }

    public int id_EnemyDeath;
    public int user_id;
    public int entity_id;
    public string timestamp;

    public float posX;
    public float posY;
    public float posZ;

    public string deathCause;
}

public class PlayerDeathData
{
    public PlayerDeathData()
    {

    }

    public int id_PlayerDeath;
    public int user_id;
    public string timestamp;

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
    public int user_id;
    public string timestamp;

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
    [SerializeField] string playerDeathURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerDeath.php";
    [SerializeField] string playerTrackURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerTrack.php";

    public List<PlayerGetsDamageData> PlayerGetsDamageDataList = new();
    public List<EnemyDeathData> EnemyGetsDamageDataList = new();
    public List<PlayerDeathData> PlayerDeathDataList = new();
    public List<SessionsData> SessionsDataList = new();
    public List<PlayerTrackData> PlayerTrackDataList = new();

    public static ReceiveData Instance;
    public void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
        form.AddField("isEnemy", true.ToString());

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
                    EnemyDeathData dataObject = JsonUtility.FromJson<EnemyDeathData>(row);
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
        form.AddField("isEnemy", false.ToString());

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
