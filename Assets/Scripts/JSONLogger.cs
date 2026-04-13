using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Script para registrar cada vez que el usuario dispara y en qué momento

[Serializable]
public class ShotData
{
    public string timestamp;        //TimeStamp del tiempo real
}

[Serializable]
public class ShotLog
{
    public List<ShotData> shots = new List<ShotData>();     //Para almacenar los shots
}

public class JSONLogger : MonoBehaviour
{
    private static string filePath => Path.Combine(Application.dataPath, "JSON/shots.json");        //datapath me guarda el fichero en la ruta que diga el string (en este caso JSON/shots.json)
    private ShotLog log = new ShotLog();    //Para almacenar los shots

    private void Awake()
    {
        Load();     //Si queremos sobreescribir, hacemos esta llamada
        //StartNewLog();      //Si queremos crear un JSON nuevo, hacemos esta llamada
    }

    private void StartNewLog()
    {
        log = new ShotLog(); // Crea una nueva lista vacía
        Save();              // Guarda un archivo vacío (opcional)
    }

    public void RegisterShot()
    {
        ShotData newShot = new ShotData { timestamp = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy") };       //Formato hora:minuto:segundo dia-mes-año
        log.shots.Add(newShot);
        Save();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(log, true);        //Guardar en el JSON
        File.WriteAllText(filePath, json);
        Debug.Log($"Shot registrado: {filePath}");
    }

    private void Load()         //Como primero se hace la llamada al Load, primero cargar el archivo y va incluyendo llamadas de disparo, por eso no borra los disparos anteriores
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);   //Carga JSON
            log = JsonUtility.FromJson<ShotLog>(json);
        }
        else
        {
            // Si el archivo no existe, crea un nuevo archivo vacío
            log = new ShotLog(); // Lista vacía
            Save();              // Guarda un archivo nuevo
            Debug.Log("No se encontró JSON. Se ha creado uno nuevo vacío.");
        }
    }
}