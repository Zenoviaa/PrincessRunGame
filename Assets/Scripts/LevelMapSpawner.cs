using System;
using System.Collections;
using UnityEngine;

public class LevelMapSpawner : MonoBehaviour
{
    private LevelMap _lastLevelMap;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LevelMap[] _levelMapPool;
    private void Update()
    {
        float xDist = _spawnPoint.transform.position.x - _lastLevelMap.transform.position.x;
        xDist = MathF.Abs(xDist);
        if(_lastLevelMap == null || xDist >= _lastLevelMap.tilemap.localBounds.size.x)
        {
            //Spawn Next one
            SpawnLevelMap();
        }
    }

    private void SpawnLevelMap()
    {
        LevelMap levelMapToSpawn = _levelMapPool[UnityEngine.Random.Range(0, _levelMapPool.Length)];
        _lastLevelMap = Instantiate(levelMapToSpawn, _spawnPoint.transform.position, Quaternion.identity);
    }
}