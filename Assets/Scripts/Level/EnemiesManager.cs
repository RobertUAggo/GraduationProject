using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private float despawnDelay = 2;
    [SerializeField] private int[] capacities = new int[] { 25, 15, 10 };
    [SerializeField] private Enemy[] enemiesPrefabs;
    private Pool<Enemy>[] _enemiesPool;
    public void Init()
    {
        _enemiesPool = new Pool<Enemy>[enemiesPrefabs.Length];
        for (int i = 0; i < _enemiesPool.Length; i++)
        {
            _enemiesPool[i] = new Pool<Enemy>(enemiesPrefabs[i],
                transform,
                capacities.Length < i ? capacities[i] : capacities[capacities.Length - 1]);
            _enemiesPool[i].OnCreate.AddListener(enemy => OnCreateEnemy(i, enemy));
            _enemiesPool[i].Init();
        }
    }
    private void Start()
    {
        
    }
#if UNITY_EDITOR
    [ContextMenu(nameof(TestSpawnEnemy))]
    private void TestSpawnEnemy()
    {
        SpawnEnemy(1, 0, Vector3.zero);
    }
#endif
    public void SpawnEnemy(int type, int level, Vector3 position)
    {
        var newInstance = _enemiesPool[type].Take();
        newInstance.transform.position = position;
        newInstance.NavAgent.enabled = true;
        newInstance.gameObject.SetActive(true);
        newInstance.OnDie.AddListener(() => DespawnEnemy(type, newInstance));
        newInstance.Init(level);
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
