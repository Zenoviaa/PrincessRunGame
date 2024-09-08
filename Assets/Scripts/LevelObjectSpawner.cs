using UnityEngine;

public class LevelObjectSpawner : MonoBehaviour
{
    private float _globalTimer;
    private float _timer;
    private bool _hasCompletedLevel;

    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private float _levelDuration;
    [SerializeField] private GameObject[] _spawnPrefabs;
    [SerializeField] private GameObject _levelEndPrefab;
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _timeBetweenSpawns)
        {
            SpawnPrefab();
            _timer = 0;
        }

        _globalTimer += Time.deltaTime;
        if(_globalTimer >= _levelDuration && !_hasCompletedLevel)
        {
            _hasCompletedLevel = true;
            SpawnEndLevelPrefab();
            _globalTimer = 0;
        }
    }

    private void SpawnPrefab()
    {
        float y = Random.Range(_bottomBound.transform.position.y, _topBound.transform.position.y);
        float x = _bottomBound.transform.position.x;
        Vector2 spawnPos = new Vector2(x, y);
        GameObject prefabToSpawn = _spawnPrefabs[Random.Range(0, _spawnPrefabs.Length)];
        Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);
    }

    private void SpawnEndLevelPrefab()
    {
        Instantiate(_levelEndPrefab, _bottomBound.transform.position, _levelEndPrefab.transform.rotation);
    }
}
