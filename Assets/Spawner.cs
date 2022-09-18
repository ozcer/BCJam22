using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public List<GameObject> racoonPrefabs;
    public Bounds spawnArea;
    
    #region singleton
    static Spawner _instance;
    void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public static Spawner Get()
    {
        return _instance;
    }
    #endregion

    
    private void Start()
    {
        spawnArea = GetComponent<Collider2D>().bounds;
    }


    public void SpawnRandom()
    {
        GameObject chosen = racoonPrefabs[Random.Range(0, racoonPrefabs.Count)];
        Vector2 randomPoint = RandomPointInBounds(spawnArea);
        GameObject spawnedRacoon = Instantiate(chosen,randomPoint, quaternion.identity);
    }
    
    public static Vector2 RandomPointInBounds(Bounds bounds) {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
