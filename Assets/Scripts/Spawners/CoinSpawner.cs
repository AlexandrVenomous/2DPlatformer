using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform _spawnPointsParent;

    private readonly int _poolCapacity = 10;
    private readonly int _poolMaxSize = 100;
    private readonly float _repeatRate = 0f;

    private ObjectPool<Coin> _pool;
    private List<Transform> _spawnPoints;

    private void Awake()
    {
        _pool = InitiatePool(_prefab);
        _spawnPoints = InitiateSpawnPoints(_spawnPointsParent);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private ObjectPool<Coin> InitiatePool(Coin prefab)
    {
        return new ObjectPool<Coin>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: (obj) => Get(obj),
            actionOnRelease: (obj) => Release(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Get(Coin obj)
    {
        obj.transform.position = GetRandomSpawnPoint().position;
        obj.gameObject.SetActive(true);
        obj.Collected += Release;
    }

    private void Release(Coin obj)
    {
        _spawnPoints.Add(obj.transform);
        obj.gameObject.SetActive(false);
        obj.Collected -= Release;
    }

    private List<Transform> InitiateSpawnPoints(Transform parent)
    {
        List<Transform> spawnPoints = new();

        for (int i = 0; i < parent.childCount; i++)
            spawnPoints.Add(parent.GetChild(i));

        return spawnPoints;
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (_spawnPoints.Count > 0)
        {
            _pool.Get();

            yield return wait;
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[index];

        _spawnPoints.RemoveAt(index);

        return spawnPoint;
    }
}
