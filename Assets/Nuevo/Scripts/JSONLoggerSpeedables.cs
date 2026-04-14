using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;        // Para LINQ
using UnityEngine;

[Serializable]
public class SpeedablePickupData
{
    public string timestamp;    // Hora real en la que se recogió
}

[Serializable]
public class SpeedablePickupLog
{
    public List<SpeedablePickupData> pickups = new List<SpeedablePickupData>();
}

public class JSONLoggerSpeedables : MonoBehaviour
{
    private static string filePath => Path.Combine(Application.dataPath, "JSON/speedables.json");

    private SpeedablePickupLog log = new SpeedablePickupLog();

    private void Awake()
    {
        Load();
    }

    // -------------------------
    //      REGISTRO
    // -------------------------
    public void RegisterPickup()
    {
        SpeedablePickupData newPickup = new SpeedablePickupData();
        newPickup.timestamp = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");

        log.pickups.Add(newPickup);
        Save();
    }

    // -------------------------
    //      GUARDAR / CARGAR
    // -------------------------
    private void Save()
    {
        string json = JsonUtility.ToJson(log, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Speedable recogido: " + filePath);
    }

    private void Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            log = JsonUtility.FromJson<SpeedablePickupLog>(json);
        }
        else
        {
            log = new SpeedablePickupLog();
            Save();
            Debug.Log("No se encontró speedables.json. Se ha creado uno nuevo vacío.");
        }
    }

    // -------------------------
    //      CONSULTAS LINQ
    // -------------------------

    //public int TotalPickups()
    //{
    //    return log.pickups.Count;
    //}

    //public string LastPickupTime()
    //{
    //    if (log.pickups.Count == 0)
    //        return null;

    //    SpeedablePickupData last = log.pickups.Last();
    //    return last.timestamp;
    //}
}