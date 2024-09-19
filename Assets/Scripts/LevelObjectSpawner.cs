using System;
using UnityEngine;

public class LevelObjectSpawner : MonoBehaviour
{
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
    [SerializeField] private GameObject[] _spawnPrefabs;
    [SerializeField] private GameObject _levelEndPrefab;
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;

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
        GameObject prefabToSpawn = _spawnPrefabs[UnityEngine.Random.Range(0, _spawnPrefabs.Length)];
        Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);
    }

    private void SpawnEndLevelPrefab()
    {
        Instantiate(_levelEndPrefab, _bottomBound.transform.position, _levelEndPrefab.transform.rotation);
    }
}
