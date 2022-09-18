using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public List<GameObject> racoonPrefabs;
    public List<Collider2D> colliders;
    
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


    public void SpawnRandom()
    {
        GameObject chosen = racoonPrefabs[Random.Range(0, racoonPrefabs.Count)];
        
        Collider2D randomCollider = colliders[Random.Range(0, colliders.Count)];
        Vector2 randomPoint = RandomPointInBounds(randomCollider.bounds);
        GameObject spawnedRacoon = Instantiate(chosen,randomPoint, quaternion.identity);
    }
    
    public static Vector2 RandomPointInBounds(Bounds bounds) {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
