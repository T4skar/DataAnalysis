using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class HeatMapGenerator : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        try
        {
            string jsonPath = "PlayerPathJson";
            string txtPath = "PlayerPathTxt";

            SerializeJsonList3(dataarray, jsonPath);
            JsonToTxtConverter(jsonPath, txtPath);
            Debug.Log("Ha FUNCIONADO el escribir en JASON");
        }
        catch

        {
            Debug.Log("Ha fallado el escribir en JASON");
        }
    }



    //[SerializeField]
    //public List<TestClass> testlist = new List<TestClass>();

    public TestClassList testlist = new TestClassList();
    public TestClass[] dataarray = new TestClass[]
    {
        new TestClass{Position=new Vector3(33.23f,23,232.333f),EventName="Object Position"},
        new TestClass{Position=new Vector3(3544.23f,233,233.333f),EventName="Object Position"},
        new TestClass{Position=new Vector3(23.23f,23,2432.333f),EventName="Object Position"}
    };

    public TestClasslis2 testClasslis2 = new TestClasslis2();

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

    void SerializeJson()
    {
        string filePath = "Assets/playerData2.json";
        var t = new TestClass();
        string a;

        t.Position = new Vector3(12.123f, 23.2f, 333.3f);
        t.EventName = "Object Position";
        // a = "\"Position\":{\"x\":" + t.posX.ToString() + ",\"y\":" + t.posY.ToString() + ",\"z\":" + t.posX.ToString() + "},\"EventName\":\"Object Position\"";
        string json = JsonUtility.ToJson(t);
        Stream stream = new MemoryStream();
        //BinaryWriter writer = new BinaryWriter(stream);
        //writer.Write(json);
        StreamWriter writer2 = new StreamWriter("Assets/playerData2.json", true);
        writer2.Write(json);
        // Path to save the JSON file

        // Write the JSON string to a text file
        File.WriteAllText(filePath, json);

        // Debug.Log("JSON data saved to: " + filePath);
    }
    void SerializeJsonList()
    {

        // a = "\"Position\":{\"x\":" + t.posX.ToString() + ",\"y\":" + t.posY.ToString() + ",\"z\":" + t.posX.ToString() + "},\"EventName\":\"Object Position\"";
        string json = JsonUtility.ToJson(testlist);
        Stream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json);

        // Path to save the JSON file
        string filePath = /*Application.persistentDataPath +*/ "Assets/playerDataList.json";

        // Write the JSON string to a text file
        File.WriteAllText(filePath, json);

        Debug.Log("JSON data saved to: " + filePath);
    }
    void SerializeJsonList2()
    {

        // a = "\"Position\":{\"x\":" + t.posX.ToString() + ",\"y\":" + t.posY.ToString() + ",\"z\":" + t.posX.ToString() + "},\"EventName\":\"Object Position\"";
        string json = JsonUtility.ToJson(testClasslis2);
        Stream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json);

        // Path to save the JSON file
        string filePath = /*Application.persistentDataPath +*/ "Assets/playerDataList4.json";

        // Write the JSON string to a text file
        File.WriteAllText(filePath, json);

        Debug.Log("JSON data saved to: " + filePath);
    }


}
[System.Serializable]
public class TestClass
{
    //public float posX;
    //public float posY;
    //public float posZ;

    public Vector3 Position;
    public string EventName;
}
[System.Serializable]
public class TestClasslis2
{
    //public float posX;
    //public float posY;
    //public float posZ;

    public List<Vector3> Position;
    public List<string> EventName;
}
[System.Serializable]
public class TestClassList
{
    public TestClass[] test;
}