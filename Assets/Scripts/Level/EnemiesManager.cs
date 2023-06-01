using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesManager : MonoBehaviour
{
    [Serializable]
    private class EnemyTypeSettings
    {
        [SerializeField] public int Capacity = 25;
        [SerializeField] public Enemy Prefab;
        [SerializeField] public AnimationCurve LevelCurve;
        [SerializeField] public AnimationCurve SpawnDelayCurve;
    }
    [SerializeField] private EnemyTypeSettings[] enemiesTypeSettings;
    [SerializeField] private BaseSpawner spawner;
    [SerializeField] private float maxDistWarp = 2;
    [SerializeField] private float despawnDelay = 2;
    private Pool<Enemy>[] _enemiesPool;
    public void Init()
    {
        _enemiesPool = new Pool<Enemy>[enemiesTypeSettings.Length];
        for (int i = 0; i < _enemiesPool.Length; i++)
        {
            _enemiesPool[i] = new Pool<Enemy>(enemiesTypeSettings[i].Prefab,
                transform,
                enemiesTypeSettings[i].Capacity);
            _enemiesPool[i].OnCreate.AddListener(enemy => OnCreateEnemy(i, enemy));
            _enemiesPool[i].Init();
        }
    }
    private void Start()
    {
        for (int i = 0; i < enemiesTypeSettings.Length; i++)
        {
            StartCoroutine(C_SpawnEnemyTypeLogic(i));
        }
    }
    private IEnumerator C_SpawnEnemyTypeLogic(int type)
    {
        while(Level.Instance.PlayerController.Player.IsAlive)
        {
            int level = Mathf.RoundToInt(enemiesTypeSettings[type].LevelCurve.Evaluate(Level.Instance.PlayTime));
            yield return new WaitForSeconds(enemiesTypeSettings[type].SpawnDelayCurve.Evaluate(Level.Instance.PlayTime));
            Vector3 randPoint = spawner.GetRandPoint();
            NavMeshHit hit;
            while (NavMesh.SamplePosition(randPoint, out hit, maxDistWarp, NavMesh.AllAreas) == false)
            {
                randPoint = spawner.GetRandPoint();
            }
            randPoint = hit.position;
            SpawnEnemy(type, level, randPoint);
        }
    }
#if UNITY_EDITOR
    [ContextMenu(nameof(TestSpawnEnemy))]
    private void TestSpawnEnemy()
    {
        SpawnEnemy(1, 0, Vector3.zero);
    }
#endif
    public Enemy SpawnEnemy(int type, int level, Vector3 position)
    {
        var newInstance = _enemiesPool[type].Take();
        newInstance.transform.position = position;
        newInstance.NavAgent.enabled = true;
        newInstance.gameObject.SetActive(true);
        newInstance.OnDie.AddListener(() => DespawnEnemy(type, newInstance));
        newInstance.Init(level);
        return newInstance;
    }
    private void DespawnEnemy(int type, Enemy instance)
    {
        StartCoroutine(C_DespawnEnemy(type, instance));
    }
    private IEnumerator C_DespawnEnemy(int type, Enemy instance)
    {
        yield return new WaitForSeconds(despawnDelay);
        instance.gameObject.SetActive(false);
        instance.NavAgent.enabled = false;
        _enemiesPool[type].BackToQueue(instance);
    }
    private void OnCreateEnemy(int type, Enemy instance)
    {
        instance.gameObject.SetActive(false);
        instance.NavAgent.enabled = false;
    }
}
