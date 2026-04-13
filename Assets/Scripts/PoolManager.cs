using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, List<GameObject>> pool;
    Transform poolParent;

    private void Awake()
    {
        this.pool = new Dictionary<string, List<GameObject>>();
        this.poolParent = new GameObject("Pool parent").transform;
    }

    public void Load(GameObject prefab, int quantity = 1)
    {
        var goName = prefab.name;
        if(!pool.ContainsKey(goName))
        {
            pool[goName] = new List<GameObject>();
        }
        for (int i = 0; i < quantity; i++) 
        {
            var go = Instantiate(prefab);
            go.name = goName;
            go.transform.SetParent(poolParent);
            go.SetActive(false);
            pool[goName].Add(go);
        }
    }

    public GameObject Spawn( GameObject prefab ) 
    {
        if(!pool.ContainsKey(prefab.name) || pool[prefab.name].Count == 0)
        {
            Load(prefab, 1);
        }
        var l = pool[prefab.name];
        var go = l[0];
        l.RemoveAt(0);
        go.SetActive(true);
        go.transform.SetParent(null, false);
        foreach(var spawnable in go.GetComponents<ISpawnable>())
        {
            if (spawnable != null)
                spawnable.OnSpawn();
        }
        return go;
    }
    
    public void Despawn(GameObject go)
    {
        if(!pool.ContainsKey(go.name))
        {
            pool[go.name] = new List<GameObject>();
        }
        foreach (var spawnable in go.GetComponents<ISpawnable>())
        {
            if(spawnable  != null)
                spawnable.OnDespawn();
        }
        go.SetActive(false);
        go.transform.SetParent(poolParent, false);
        pool[go.name].Add(go);
    }

    public bool IsObjectmanagedByPoolManager(GameObject go)
    {
        return pool.ContainsKey(go.name);
    }
}
