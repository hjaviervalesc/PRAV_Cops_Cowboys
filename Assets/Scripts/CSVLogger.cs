using System;
using System.IO;
using System.Text;
using UnityEngine;

// Script para registrar la posición del player cada 5 segundos en un CSV

public class CSVLogger : MonoBehaviour
{
    private static string folderPath => Path.Combine(Application.dataPath, "CSV");      //Path para almacenarlo
    private static string filePath => Path.Combine(folderPath, "player_positions.csv");     //Fichero. Esto se puede hace tambien asi para los JSON

    public Transform player;  // Aquí asignas el Player desde el Inspector

    private float timeBetweenLogs = 5f;
    private float timer = 0f;

    private bool headerWritten = false;

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError("Player no asignado en PlayerPositionLogger.");
            enabled = false;
            return;
        }

        // Descomenta UNA de las siguientes dos líneas según el comportamiento que quieras:

        Load();             // Opción A: seguir usando el mismo archivo
        // StartNewLog();   // Opción B: crear un nuevo archivo vacío cada vez

        // Iniciar cabecera si el archivo es nuevo
        if (!File.Exists(filePath))
        {
            WriteHeader();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenLogs)
        {
            timer = 0f;
            LogPosition();
        }
    }

    private void StartNewLog()
    {
        Directory.CreateDirectory(folderPath);
        File.WriteAllText(filePath, ""); // Borra el contenido del archivo
        WriteHeader();
        Debug.Log("Archivo CSV nuevo creado.");
    }

    private void Load()
    {
        Directory.CreateDirectory(folderPath);

        if (!File.Exists(filePath))
        {
            WriteHeader(); // Si no existe, se escribe cabecera
            Debug.Log("Archivo CSV no encontrado. Se ha creado uno nuevo.");
        }
        else
        {
            Debug.Log("Archivo CSV existente cargado.");
        }
    }

    private void WriteHeader()
    {
        string header = "Nombre del objeto; ;Tiempo;X;Y;Z";
        File.AppendAllText(filePath, header + Environment.NewLine, Encoding.UTF8);
        headerWritten = true;
    }

    private void LogPosition()
    {
        Vector3 pos = player.position;
        string timestamp = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");        //mismo formato que el JSON

        string line = $"{player.name}; ;\"'{timestamp}\";{pos.x:F2};{pos.y:F2};{pos.z:F2}";      //Lo que va a ir en la linea. Si dejamos el ' en \"'{timestamp}\", se ve en el CSV, pero matiene 
                                                                                                //el formato (hora:minuto:segundo dia-mes-año). Si quitamos el ', no se ve en el CSV, pero
                                                                                                //pierde el formato (aparece como dia-mes-año hora:minuto:segundo). Es cosa del CSV, no de Unity

        File.AppendAllText(filePath, line + Environment.NewLine, Encoding.UTF8);        //NewLine es salto de linea. UTF8 es la codificacion de texto y soporta caracteres
        Debug.Log($"Posición registrada en CSV: {filePath}");
    }
}
