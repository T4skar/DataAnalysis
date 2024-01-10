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
            Debug.Log("Ha FUNCIONADO el escribir en JASON");
        }
        catch 
        {
            Debug.Log("Ha fallado el escribir en JASON");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SerializeJson()
    {
        var t = new TestClass();
        string a;
        //t.posX = 67.231f;
        //t.posY = 342.541f;
        //t.posZ = 71.231f;

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
}
class TestClass
{
    //public float posX;
    //public float posY;
    //public float posZ;

    public Vector3 Position;
    public string EventName;
}
