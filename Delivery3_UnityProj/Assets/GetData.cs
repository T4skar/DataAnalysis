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

    [Header("Upload data to SQLazo")]
    public bool wantToGetData = true;

    [Header("Links")]
    //[SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xavierlm9/Sessions.php";
    [SerializeField] string playerGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerGetsDmg.php";
    [SerializeField] string playerDeathURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerDeath.php";
    [SerializeField] string playerTrackURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerTrack.php";

    [Header("Entities")]
    [SerializeField]
    Damageable Ellen;

    [SerializeField]
    List<Damageable> enemies = new();

    [Header("Player Tracking")]
    [SerializeField][Tooltip("Delay in sec. between player gets tracked")] float trackPlayerCooldown = 1f;
    float trackCounter;

    [Header("Sessions")]
    public int userId = 69;
    [SerializeField] int sessionId;
    int entityId;

    void OnEnable()
    {
        if (!wantToGetData) return;

        userId = UnityEngine.Random.Range(0, 99999);

        Ellen.onDamageMessageReceivers.Add(this);

        foreach (Damageable damageable in enemies)
        {
            damageable.onDamageMessageReceivers.Add(this);
        }

        trackCounter = trackPlayerCooldown;

        StartCoroutine(Send_Sessions(true));
    }

    void OnDisable()
    {
        StartCoroutine(Send_Sessions(false));
    }

    private void Update()
    {
        if (trackPlayerCooldown < 0 && wantToGetData)
        {
            trackPlayerCooldown = trackCounter;
            StartCoroutine(Send_PlayerTrack());
        }
        else
        {
            trackPlayerCooldown -= Time.deltaTime;
        }
    }

    public IEnumerator Send_Sessions(bool isStarting)
    {
        Debug.Log("Session Data");

        WWWForm form = new();

        form.AddField("methodToCall", "Set Session");

        form.AddField("user", userId);
        form.AddField("session", sessionId);
        form.AddField("starting", isStarting.ToString());
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(playerDeathURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        Debug.Log("Message recieved");

        switch (type)
        {
            case MessageType.DAMAGED:

                StartCoroutine(Send_GetsDamage((Damageable)sender, (Damageable.DamageMessage)msg));
                break;

            case MessageType.DEAD:

                StartCoroutine(Send_Death((Damageable)sender, (Damageable.DamageMessage)msg));
                break;
        }
    }

    public IEnumerator Send_GetsDamage(Damageable sender, Damageable.DamageMessage msg) // Este solo lo activa Ellen Jaeger
    {
        Debug.Log(sender.name + " needs a paracetamol");

        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");
        form.AddField("user", userId);
        form.AddField("posX", (int)sender.transform.position.x);
        form.AddField("posY", (int)sender.transform.position.y);
        form.AddField("posZ", (int)sender.transform.position.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("damageCause", msg.throwing ? "Distance Attack" : "Melee Attack");

        UnityWebRequest www = UnityWebRequest.Post(playerGetsDmgURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public IEnumerator Send_Death(Damageable sender, Damageable.DamageMessage msg)
    {
        Debug.Log(sender.name + " is Dead because " + msg.damager.name);

        if (sender != Ellen)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (sender == enemies[i])
                {
                    entityId = i;
                    break;
                }
            }
        }

        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");
        form.AddField("user", userId);
        form.AddField("entityId", entityId);
        form.AddField("entity", sender.name);
        form.AddField("posX", (int)sender.transform.position.x);
        form.AddField("posY", (int)sender.transform.position.y);
        form.AddField("posZ", (int)sender.transform.position.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        form.AddField("deathCause", msg.damager.name);

        UnityWebRequest www = UnityWebRequest.Post(playerDeathURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public IEnumerator Send_PlayerTrack()
    {
        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");
        form.AddField("user", userId);
        form.AddField("posX", (int)Ellen.transform.position.x);
        form.AddField("posY", (int)Ellen.transform.position.y);
        form.AddField("posZ", (int)Ellen.transform.position.z);
        form.AddField("rotX", (int)Ellen.transform.rotation.eulerAngles.x);
        form.AddField("rotY", (int)Ellen.transform.rotation.eulerAngles.y);
        form.AddField("rotZ", (int)Ellen.transform.rotation.eulerAngles.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(playerTrackURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    void CheckPHPLog(UnityWebRequest www)
    {
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log("Error: " + www.error);
        else
            Debug.Log(www.downloadHandler.text);
    }
}