using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class HeatMapGenerator : MonoBehaviour
{

    public TestClass[] dataarray = new TestClass[]
   {
        new TestClass{Position=new Vector3(33.23f,23,232.333f),EventName="Object Position"},
        new TestClass{Position=new Vector3(3544.23f,233,233.333f),EventName="Object Position"},
        new TestClass{Position=new Vector3(23.23f,23,2432.333f),EventName="Object Position"}
   };

    TestClass[] PlayerGetsDamage = null;
    List<TestClass> PlayerGetsDamage2 = new();
    TestClass[] EnemyGetsDamage = null;
    TestClass[] PlayerDeath = null;
    TestClass[] Sessions = null;
    TestClass[] PlayerTrack = null;

    public string eventName = "Object Position";
    public string folderPath = "Json_TXT/";

    public string trackJsonPath = "Json_TXT/trackJson";
    public string trackTxtPath = "Json_TXT/trackTxt";

    public string pDeathJsonPath = "Json_TXT/pDeathJson";
    public string pDeathTxtPath = "Json_TXT/pDeathTxt";

    public string eDmgJsonPath = "Json_TXT/eDmgJson";
    public string eDmgTxtPath = "Json_TXT/eDmgTxt";

    public string pDmgJsonPath = "Json_TXT/pDmgJson";
    public string pDmgTxtPath = "Json_TXT/pDmgTxt";

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void GenerateFiles()
    {
        try
        {

            int dmgCount = 0;
            PlayerGetsDamage = new TestClass[ReceiveData.Instance.PlayerGetsDamageDataList.Count];
            foreach (PlayerGetsDamageData item in ReceiveData.Instance.PlayerGetsDamageDataList)
            {
                //PlayerGetsDamage[dmgCount]= ( Position = new Vector3(item.posX, item.posY, item.posZ),EventName = eventName )
                PlayerGetsDamage[dmgCount].Position = new Vector3(item.posX, item.posY, item.posZ);
                PlayerGetsDamage[dmgCount].EventName = eventName;
                
                dmgCount++;
            }
            Debug.Log("Lista de player damage bien");
            
            Debug.Log("Lista to Json todo bien");

            SerializeJsonList3(PlayerGetsDamage, pDmgJsonPath);
            JsonToTxtConverter(pDmgJsonPath, pDmgTxtPath);

            Debug.Log("Ha FUNCIONADO el escribir en JASON");
        }
        catch

        {
            Debug.Log("Ha fallado el escribir en JASON");
        }

        //try
        //{
        //    int deathCount = 0;
        //    foreach (PlayerDeathData item in ReceiveData.Instance.PlayerDeathDataList)
        //    {
        //        PlayerDeath[deathCount].Position = new Vector3(item.posX, item.posY, item.posZ);
        //        PlayerDeath[deathCount].EventName = eventName;
        //        deathCount++;

        //    }
        //    Debug.Log("Lista de player death bien");
        //    Debug.Log("Lista to Json todo bien");

        //    SerializeJsonList3(PlayerDeath, pDeathJsonPath);
        //    JsonToTxtConverter(pDeathJsonPath, pDeathTxtPath);

        //    Debug.Log("Ha FUNCIONADO el escribir en JASON");
        //}
        //catch

        //{
        //    Debug.Log("Ha fallado el escribir en JASON");
        //}
        //try
        //{ 
        //    int enemyCount = 0;
        //    foreach (EnemyGetsDamageData item in ReceiveData.Instance.EnemyGetsDamageDataList)
        //    {
        //        EnemyGetsDamage[enemyCount].Position = new Vector3(item.PosX, item.PosY, item.PosZ);
        //        EnemyGetsDamage[enemyCount].EventName = eventName;
        //        enemyCount++;
        //    }
        //    Debug.Log("Lista de enemigo damage bien");

        //    Debug.Log("Lista to Json todo bien");

        //    SerializeJsonList3(EnemyGetsDamage, eDmgJsonPath);
        //    JsonToTxtConverter(eDmgJsonPath, eDmgTxtPath);

        //    Debug.Log("Ha FUNCIONADO el escribir en JASON");
        //}
        //catch
        //{
        //    Debug.Log("Ha fallado el escribir en JASON");
        //}

        //try
        //{
        //    int trackCount = 0;
        //    foreach (PlayerTrackData item in ReceiveData.Instance.PlayerTrackDataList)
        //    {
        //        PlayerTrack[trackCount].Position = new Vector3(item.Pos_X, item.Pos_Y, item.Pos_Z);
        //        PlayerTrack[trackCount].EventName = eventName;
        //        trackCount++;
        //    }
        //    Debug.Log("Lista de player track bien");

        //    Debug.Log("Lista to Json todo bien");

        //    SerializeJsonList3(PlayerTrack, trackJsonPath);
        //    JsonToTxtConverter(trackJsonPath, trackTxtPath);

        //    Debug.Log("Ha FUNCIONADO el escribir en JASON");
        //}
        //catch

        //{
        //    Debug.Log("Ha fallado el escribir en JASON");
        //}

        /*
        try
        {

            int dmgCount = 0;
            foreach (PlayerGetsDamageData item in ReceiveData.Instance.PlayerGetsDamageDataList)
            {
                PlayerGetsDamage[dmgCount].Position = new Vector3(item.posX, item.posY, item.posZ);
                PlayerGetsDamage[dmgCount].EventName = eventName;
                dmgCount++;
            }
            Debug.Log("Lista de player damage bien");
            int deathCount = 0;
            foreach (PlayerDeathData item in ReceiveData.Instance.PlayerDeathDataList)
            {
                PlayerDeath[deathCount].Position = new Vector3(item.posX, item.posY, item.posZ);
                PlayerDeath[deathCount].EventName = eventName;
                deathCount++;

            }
            Debug.Log("Lista de player death bien");
            int enemyCount = 0;
            foreach (EnemyGetsDamageData item in ReceiveData.Instance.EnemyGetsDamageDataList)
            {
                EnemyGetsDamage[enemyCount].Position = new Vector3(item.PosX, item.PosY, item.PosZ);
                EnemyGetsDamage[enemyCount].EventName = eventName;
                enemyCount++;
            }
            Debug.Log("Lista de enemigo damage bien");
            int trackCount = 0;
            foreach (PlayerTrackData item in ReceiveData.Instance.PlayerTrackDataList)
            {
                PlayerTrack[trackCount].Position = new Vector3(item.Pos_X, item.Pos_Y, item.Pos_Z);
                PlayerTrack[trackCount].EventName = eventName;
                trackCount++;
            }
            Debug.Log("Lista de player track bien");

            Debug.Log("Lista to Json todo bien");
            //foreach (SessionsData item in ReceiveData.Instance.SessionsDataList)
            //{
            //    Sessions[1].Position = new Vector3(item., item.Pos_Y, item.Pos_Z);
            //    Sessions[1].EventName = eventName;
            //}

            SerializeJsonList3(PlayerTrack, trackJsonPath);
            JsonToTxtConverter(trackJsonPath, trackTxtPath);

            SerializeJsonList3(PlayerGetsDamage, pDmgJsonPath);
            JsonToTxtConverter(pDmgJsonPath, pDmgTxtPath);

            SerializeJsonList3(PlayerDeath, pDeathJsonPath);
            JsonToTxtConverter(pDeathJsonPath, pDeathTxtPath);

            SerializeJsonList3(EnemyGetsDamage, eDmgJsonPath);
            JsonToTxtConverter(eDmgJsonPath, eDmgTxtPath);


            Debug.Log("Ha FUNCIONADO el escribir en JASON");
        }
        catch

        {
            Debug.Log("Ha fallado el escribir en JASON");
        }
        */

    }

    //private void ListToArray<T>(List<T> list, TestClass[] array)
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        array[i] = list.
    //    }
    //}

    void SerializeJsonList3(TestClass[] itemJson, string pathjson)
    {

        string filePathJson = Application.dataPath + "/" + pathjson + ".json";


        using (StreamWriter writer = new StreamWriter(filePathJson))
        {
            foreach (TestClass item in dataarray)
            {

                string json = JsonUtility.ToJson(item);

                writer.WriteLine(json);
            }
        }


        Debug.Log("JSON data saved to: " + filePathJson);
    }

    private static void JsonToTxtConverter(string PathJson, string PathTxt)
    {

        string filePathJson = Application.dataPath + "/" + PathJson + ".json";
        string filePathTxt = Application.dataPath + "/" + PathTxt + ".json";

        // Cargar el contenido del archivo JSON como una cadena
        string jsonContent = File.ReadAllText(filePathJson);

        // Guardar la cadena JSON como un archivo de texto
        File.WriteAllText(filePathTxt, jsonContent);

        Debug.Log("TXT data saved to: " + filePathTxt);
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
    public TestClass[] arrayList;

}