using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class jsonToTxt : MonoBehaviour
{
    // Ruta del archivo JSON - ahora es pública y editable desde el Inspector
    public string jsonFilePath = "heatmap utilities/Test-data/Test.json";

    // Ruta de destino para el archivo de texto - ahora es pública y editable desde el Inspector
    public string txtFilePath = "heatmap utilities/Test-data/convertedTextFile.txt";

    void Start()
    {
        // Construir la ruta completa del archivo JSON
        string fullJsonFilePath = Application.dataPath + "/" + jsonFilePath;

        // Construir la ruta completa del archivo de texto
        string fullTxtFilePath = Application.dataPath + "/" + txtFilePath;

        // Cargar el contenido del archivo JSON como una cadena
        string jsonContent = File.ReadAllText(fullJsonFilePath);

        // Guardar la cadena JSON como un archivo de texto
        File.WriteAllText(fullTxtFilePath, jsonContent);

        Debug.Log("Conversión completada. Archivo TXT creado en: " + fullTxtFilePath);
    }
}
