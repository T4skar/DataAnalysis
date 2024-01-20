using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour, IMessageReceiver
{
    // === Variables ===

    [SerializeField] string sessionsUrl = "https://citmalumnes.upc.es/~xavierlm9/Sessions.php";
    [SerializeField] string playerGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerGetsDmg.php";
    [SerializeField] string enemyGetsDmgURL = "https://citmalumnes.upc.es/~xavierlm9/EnemyGetsDmg.php";
    [SerializeField] string playerDeathURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerDeath.php";
    [SerializeField] string playerTrackURL = "https://citmalumnes.upc.es/~xavierlm9/PlayerTrack.php";

    [SerializeField]
    Damageable Ellen;

    [SerializeField]
    List<Damageable> enemies = new();

    [SerializeField][Tooltip("Delay in sec. between player gets tracked")] float trackPlayerCooldown = 1f;
    float trackCounter;

    [Range(0, 13)] public int userId;

    [SerializeField] int sessionId;

    void OnEnable()
    {
        Ellen.onDamageMessageReceivers.Add(this);

        foreach (Damageable damageable in enemies)
        {
            damageable.onDamageMessageReceivers.Add(this);
        }

        StartCoroutine(Send_Sessions(true));

        trackCounter = trackPlayerCooldown;

        StartCoroutine(Send_PlayerTrack());
    }

    void OnDisable()
    {
        StartCoroutine(Send_Sessions(false));
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
                
                StartCoroutine(Send_PlayerGetsDamage(sender, (Damageable.DamageMessage)msg));
                break;

            case MessageType.DEAD:
                StartCoroutine(Send_PlayerDeath(sender, (Damageable.DamageMessage)msg));
                break;

            case MessageType.RESPAWN:

                break;
        }
    }

    public IEnumerator Send_PlayerGetsDamage(object sender, Damageable.DamageMessage msg)
    {
        WWWForm form = new WWWForm();

        form.AddField("methodToCall", "Set Info");

        form.AddField("posX", (int)Ellen.transform.position.x);
        form.AddField("posY", (int)Ellen.transform.position.y);
        form.AddField("posZ", (int)Ellen.transform.position.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("damageCause", msg.throwing ? "Distance Attack" : "Melee Attack");

        UnityWebRequest www = UnityWebRequest.Post(playerGetsDmgURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public IEnumerator Send_EnemyGetsDamage(object sender, Damageable.DamageMessage msg)
    {
        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");

        form.AddField("posX", msg.damager.transform.position.x.ToString());
        form.AddField("posY", msg.damager.transform.position.y.ToString());
        form.AddField("posZ", msg.damager.transform.position.z.ToString());
        form.AddField("timeStamp", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(enemyGetsDmgURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public IEnumerator Send_PlayerDeath(object sender, Damageable.DamageMessage msg)
    {
        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");

        form.AddField("posX", (int)Ellen.transform.position.x);
        form.AddField("posY", (int)Ellen.transform.position.y);
        form.AddField("posZ", (int)Ellen.transform.position.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("damageCause", msg.throwing ? "Distance Attack" : "Melee Attack");

        UnityWebRequest www = UnityWebRequest.Post(playerDeathURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    public IEnumerator Send_PlayerTrack()
    {
        if (trackPlayerCooldown > 0)
        {
            trackPlayerCooldown = trackCounter;
            yield return null;
        }
        else
        {
            trackPlayerCooldown -= Time.deltaTime;
        }

        WWWForm form = new();

        form.AddField("methodToCall", "Set Info");

        form.AddField("posX", (int)Ellen.transform.position.x);
        form.AddField("posY", (int)Ellen.transform.position.y);
        form.AddField("posZ", (int)Ellen.transform.position.z);
        form.AddField("rotX", (int)Ellen.transform.rotation.eulerAngles.x);
        form.AddField("rotY", (int)Ellen.transform.rotation.eulerAngles.y);
        form.AddField("rotZ", (int)Ellen.transform.rotation.eulerAngles.z);
        form.AddField("timeStamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(playerDeathURL, form);

        yield return www.SendWebRequest();

        CheckPHPLog(www);
    }

    void CheckPHPLog(UnityWebRequest www)
    {
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}