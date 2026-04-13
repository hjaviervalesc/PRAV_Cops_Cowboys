using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class KillData
{
    public string timestamp;    // Hora real de la kill
}

[Serializable]
public class KillLog
{
    public List<KillData> kills = new List<KillData>();
}

public class JSONLoggerKills : MonoBehaviour
{
    private static string filePath => Path.Combine(Application.dataPath, "JSON/kills.json");
    private KillLog log = new KillLog();

    private void Awake()
    {
        Load();
    }

    private void StartNewLog()
    {
        log = new KillLog();
        Save();
    }

    // -------------------------
    // REGISTRO DE KILLS
    // -------------------------

    public void RegisterKill()
    {
        KillData newKill = new KillData
        {
            timestamp = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy")
        };

        log.kills.Add(newKill);
        Save();
    }

    // -------------------------
    // GUARDAR Y CARGAR
    // -------------------------

    private void Save()
    {
        string json = JsonUtility.ToJson(log, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"Kill registrada: {filePath}");
    }

    private void Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            log = JsonUtility.FromJson<KillLog>(json);
        }
        else
        {
            log = new KillLog();
            Save();
            Debug.Log("No se encontró kills.json. Se ha creado uno nuevo vacío.");
        }
    }

    // -------------------------
    // CONSULTAS (opcional)
    // -------------------------

    public int TotalKills()
    {
        return log.kills.Count;
    }

    public string FirstKillTime()
    {
        return log.kills.Count > 0 ? log.kills[0].timestamp : null;
    }   
}
