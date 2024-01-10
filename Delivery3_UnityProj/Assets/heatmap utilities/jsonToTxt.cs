using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class jsonToTxt : MonoBehaviour
{
    void Start()
    {
        // Ruta del archivo JSON
        string jsonFilePath = Application.dataPath + "/heatmap utilities/Test-data/Test.json";

        // Ruta de destino para el archivo de texto
        string txtFilePath = Application.dataPath + "/heatmap utilities/Test-data/convertedTextFile.txt";

        // Cargar el contenido del archivo JSON como una cadena
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Guardar la cadena JSON como un archivo de texto
        File.WriteAllText(txtFilePath, jsonContent);

        Debug.Log("Conversión completada. Archivo TXT creado en: " + txtFilePath);
    }
}
