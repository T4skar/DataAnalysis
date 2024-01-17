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

            SerializeJson();
            SerializeJsonList();
            SerializeJsonList2();
            Debug.Log("Ha FUNCIONADO el escribir en JASON");
        }
        catch 

        {
            Debug.Log("Ha fallado el escribir en JASON");
        }
    }

    

    //[SerializeField]
    //public List<TestClass> testlist = new List<TestClass>();

    public TestClassList testlist= new TestClassList();

    public TestClasslis2 testClasslis2 = new TestClasslis2();
    void SerializeJson()
    {
        var t = new TestClass();
        string a;
       
        t.Position =  new Vector3(12.123f, 23.2f, 333.3f);
        t.EventName = "Object Position";
       // a = "\"Position\":{\"x\":" + t.posX.ToString() + ",\"y\":" + t.posY.ToString() + ",\"z\":" + t.posX.ToString() + "},\"EventName\":\"Object Position\"";
        string json = JsonUtility.ToJson(t);
        Stream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json);
        
        // Path to save the JSON file
        string filePath = /*Application.persistentDataPath +*/ "Assets/playerData2.json";

        // Write the JSON string to a text file
        File.WriteAllText(filePath,json);
        
        Debug.Log("JSON data saved to: " + filePath);
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
        File.WriteAllText(filePath,json);
        
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
        File.WriteAllText(filePath,json);
        
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