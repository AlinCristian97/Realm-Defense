using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] [Range(0, 50)] private int _poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] private float _spawnCooldown = 1f;

    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        _pool = new GameObject[_poolSize];

        for (int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }

    private void EnableObjectInPool()
    { 
        foreach (GameObject t in _pool)
        {
            if (t.activeInHierarchy) continue;
            t.SetActive(true);
            return;
        }
    }
}
