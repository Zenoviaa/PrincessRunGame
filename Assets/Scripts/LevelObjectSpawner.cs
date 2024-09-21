using System;
using UnityEngine;

public class LevelObjectSpawner : MonoBehaviour
{
    public enum SpawnLocation
    {
        Default,
        Floor,
        Ceiling
    }

    [Serializable]
    private class HazardSpawn
    {
        public GameObject prefab;
        public SpawnLocation spawnLocation;
    }

    private float _globalTimer;
    private float _timer;
    private float _collectibleTimer;
    private bool _hasCompletedLevel;
    private float _timeBetweenSpawns;

    [SerializeField] private float _minTimeBetweenSpawns = 0.75f;
    [SerializeField] private float _maxTimeBetweenSpawns = 2f;
    [SerializeField] private float _timeBetweenCollectibleSpawns;
    [SerializeField] private float _levelDuration;
    [SerializeField] private GameObject[] _collectibleSpawnPrefabs;
    [SerializeField] private HazardSpawn[] _spawnPrefabs;
    [SerializeField] private GameObject _levelEndPrefab;
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;

    [SerializeField] private Transform _floor;
    [SerializeField] private Transform _ceiling;

    public float Progress => Math.Clamp(_globalTimer / _levelDuration, 0, 1);

    private void Start()
    {
        _timeBetweenSpawns = UnityEngine.Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
    }

    private void Update()
    {
        GameManager gameManager = GameManager.Instance;
        if (!gameManager.StartedLevel)
            return;
        if (gameManager.EndedRun)
            return;
        _timer += Time.deltaTime;
        if(_timer >= _timeBetweenSpawns)
        {
            SpawnPrefab();
            _timer = 0;
        }

        _collectibleTimer += Time.deltaTime;
        if(_collectibleTimer >= _timeBetweenCollectibleSpawns)
        {
            SpawnCollectiblePrefab();
            _collectibleTimer = 0;
        }

        _globalTimer += Time.deltaTime;
        if(_globalTimer >= _levelDuration && !_hasCompletedLevel)
        {
            _hasCompletedLevel = true;
            SpawnEndLevelPrefab();
        }
    }
    private void SpawnCollectiblePrefab()
    {
        float y = UnityEngine.Random.Range(_bottomBound.transform.position.y, _topBound.transform.position.y);
        float x = _bottomBound.transform.position.x;
        Vector2 spawnPos = new Vector2(x, y);
        GameObject prefabToSpawn = _collectibleSpawnPrefabs[UnityEngine.Random.Range(0, _collectibleSpawnPrefabs.Length)];
        Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);
    }

    private void SpawnPrefab()
    {
        _timeBetweenSpawns = UnityEngine.Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
        float y = UnityEngine.Random.Range(_bottomBound.transform.position.y, _topBound.transform.position.y);
        float x = _bottomBound.transform.position.x;
        Vector2 spawnPos = new Vector2(x, y);
        var prefabToSpawn = _spawnPrefabs[UnityEngine.Random.Range(0, _spawnPrefabs.Length)];
        switch (prefabToSpawn.spawnLocation)
        {
            case SpawnLocation.Default:
                Instantiate(prefabToSpawn.prefab, spawnPos, prefabToSpawn.prefab.transform.rotation);
                break;
            case SpawnLocation.Floor:
                spawnPos = new Vector2(x, _floor.transform.position.y);
                Instantiate(prefabToSpawn.prefab, spawnPos, prefabToSpawn.prefab.transform.rotation);
                break;
            case SpawnLocation.Ceiling:
                spawnPos = new Vector2(x, _ceiling.transform.position.y);
                Instantiate(prefabToSpawn.prefab, spawnPos, prefabToSpawn.prefab.transform.rotation);
                break;
        }
    
    }

    private void SpawnEndLevelPrefab()
    {
        Instantiate(_levelEndPrefab, _bottomBound.transform.position, _levelEndPrefab.transform.rotation);
    }
}
