using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class HeatMapGenerator : MonoBehaviour
{


    public string eventName = "Object Position";
    public string folderPath = "Json_TXT/";

    public string dataPathJson = "Json_TXT/dataHeatmapJson";
    public string dataPathTxt = "Json_TXT/dataHeatmapTxt";

   

    // Start is called before the first frame update
    void Start()
    {

    }
   
    void SerializeJsonList3(TestClass[] itemJson, string pathjson)
    {

        string filePathJson = Application.dataPath + "/" + pathjson + ".json";


        using (StreamWriter writer = new StreamWriter(filePathJson))
        {

            foreach (TestClass item in itemJson)
            {

                if (item!=null)
                {
                    string json = JsonUtility.ToJson(item);

                    writer.WriteLine(json);
                } 
            }
        }
        

        Debug.Log("JSON data saved to: " + filePathJson);
    }

    private static void JsonToTxtConverter(string PathJson, string PathTxt)
    {

        string filePathJson = Application.dataPath + "/" + PathJson + ".json";
        string filePathTxt = Application.dataPath + "/" + PathTxt + ".txt";

        // Cargar el contenido del archivo JSON como una cadena
        string jsonContent = File.ReadAllText(filePathJson);

        // Guardar la cadena JSON como un archivo de texto
        File.WriteAllText(filePathTxt, jsonContent);

        Debug.Log("TXT data saved to: " + filePathTxt);
    }


    public void TakeDropdownOptions()
    {
       

        string heatmapType = ReceiveData.Instance.heatmapType.options[ReceiveData.Instance.heatmapType.value].text.ToString();

        string userID = ReceiveData.Instance.userIDDropDown.options[ReceiveData.Instance.userIDDropDown.value].text.ToString();

        int userIDInt = -1;

        if (userID != "All")
        {
            userIDInt = int.Parse(userID);
        }

        switch (heatmapType)
        {
            case "Player Tracks":


                int trackCount = 0;

                TestClass[]  PlayerTrack = new TestClass[ReceiveData.Instance.PlayerTrackDataList.Count];
                foreach (PlayerTrackData item in ReceiveData.Instance.PlayerTrackDataList)
                {
                    if (userIDInt == -1)
                    {
                        PTrackArray(trackCount, item, PlayerTrack);
                    }
                    else if (userIDInt == item.user_id)
                    {
                        PTrackArray(trackCount, item, PlayerTrack);
                    }
                    trackCount++;
                }
                Debug.Log("Lista de player track bien");

                SerializeJsonList3(PlayerTrack, dataPathJson);

                Debug.Log("Ha FUNCIONADO el escribir en JASON");
                break;

            case "Player Deaths":

                string playerDeathCause = ReceiveData.Instance.playerDeathCausesDropDown.options[ReceiveData.Instance.playerDeathCausesDropDown.value].text.ToString();

                int deathCount = 0;
                TestClass[] PlayerDeath = null;
                PlayerDeath = new TestClass[ReceiveData.Instance.PlayerDeathDataList.Count];
                foreach (PlayerDeathData item in ReceiveData.Instance.PlayerDeathDataList)
                {
                    if (userIDInt == -1)
                    {
                        pDeathArrray(playerDeathCause, deathCount, item, PlayerDeath);

                    }
                    else if (userIDInt == item.user_id)
                    {
                        pDeathArrray(playerDeathCause, deathCount, item, PlayerDeath);
                    }
                    deathCount++;

                }
                Debug.Log("Lista de player death bien");

                SerializeJsonList3(PlayerDeath, dataPathJson);


                Debug.Log("Ha FUNCIONADO el escribir en JASON");

                break;

            case "Player Gets Damage":

                string playerGetsDamageCause = ReceiveData.Instance.playerGetsDmgCausesDropDown.options[ReceiveData.Instance.playerGetsDmgCausesDropDown.value].text.ToString();

                int dmgCount = 0;
                TestClass[] PlayerGetsDamage = new TestClass[ReceiveData.Instance.PlayerGetsDamageDataList.Count];
                foreach (PlayerGetsDamageData item in ReceiveData.Instance.PlayerGetsDamageDataList)
                {
                    if (userIDInt == -1)
                    {
                        pDmgArray(playerGetsDamageCause, dmgCount, item, PlayerGetsDamage);
                    }
                    else if (userIDInt == item.user_id)
                    {
                        pDmgArray(playerGetsDamageCause, dmgCount, item, PlayerGetsDamage);
                    }
                    dmgCount++;
                }
                Debug.Log("Lista de player damage bien");

                SerializeJsonList3(PlayerGetsDamage, dataPathJson);


                Debug.Log("Ha FUNCIONADO el escribir en JASON");

                break;

            case "Enemy Deaths":

                string enemyDeaths = ReceiveData.Instance.enemyDeathCausesDropDown.options[ReceiveData.Instance.enemyDeathCausesDropDown.value].text.ToString();

                int enemyCount = 0;
                TestClass[] EnemyGetsDamage = new TestClass[ReceiveData.Instance.EnemyGetsDamageDataList.Count];
                foreach (EnemyDeathData item in ReceiveData.Instance.EnemyGetsDamageDataList)
                {
                    if (userIDInt == -1)
                    {
                        EnemyArray(enemyDeaths, enemyCount, item, EnemyGetsDamage);
                    }
                    else if (userIDInt == item.user_id)
                    {
                        EnemyArray(enemyDeaths, enemyCount, item, EnemyGetsDamage);
                    }
                    enemyCount++;
                }
                Debug.Log("Lista de enemigo damage bien");

                SerializeJsonList3(EnemyGetsDamage, dataPathJson);


                Debug.Log("Ha FUNCIONADO el escribir en JASON");
                break;
        }
        JsonToTxtConverter(dataPathJson, dataPathTxt);
    }

    private void PTrackArray(int trackCount, PlayerTrackData item, TestClass[] PlayerTrack)
    {
        PlayerTrack[trackCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
    }

    private void pDeathArrray(string playerDeathCause, int deathCount, PlayerDeathData item, TestClass[] PlayerDeath)
    {
        if (playerDeathCause == "All")
        {
            PlayerDeath[deathCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
        }
        else if (playerDeathCause == item.deathCause)
        {

            PlayerDeath[deathCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
        }
    }

    private void pDmgArray(string playerGetsDamageCause, int dmgCount, PlayerGetsDamageData item, TestClass[] PlayerGetsDamage)
    {
        if (playerGetsDamageCause == "All")
        {

            PlayerGetsDamage[dmgCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
        }
        else if (playerGetsDamageCause == item.damageCause)
        {
            PlayerGetsDamage[dmgCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
        }
    }

    private void EnemyArray(string enemyDeaths, int enemyCount, EnemyDeathData item, TestClass[] EnemyGetsDamage)
    {
        if (enemyDeaths == "All")
        {
            EnemyGetsDamage[enemyCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };

        }
        else if (enemyDeaths == item.deathCause)
        {
            EnemyGetsDamage[enemyCount] = new TestClass { Position = new Vector3(item.posX, item.posY, item.posZ), EventName = eventName };
        }
    }

}
[System.Serializable]
public class TestClass
{

    public Vector3 Position;
    public string EventName;
}

public class TableLists
{
    public int playerId;
    public string Path;
    public TestClass[] arrayList;

}