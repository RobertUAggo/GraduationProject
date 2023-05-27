using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private float despawnDelay = 2;
    [SerializeField] private int[] capacities = new int[] { 25, 15, 10 };
    [SerializeField] private Enemy[] enemiesPrefabs;
    public Pool<Enemy>[] EnemiesPool;
    private void Awake()
    {
        EnemiesPool = new Pool<Enemy>[enemiesPrefabs.Length];
        for (int i = 0; i < EnemiesPool.Length; i++)
        {
            EnemiesPool[i] = new Pool<Enemy>(enemiesPrefabs[i],
                transform,
                capacities.Length < i ? capacities[i] : capacities[capacities.Length - 1]);
            EnemiesPool[i].OnCreate.AddListener(enemy => OnCreateEnemy(i, enemy));
            EnemiesPool[i].Init();
        }
    }
    private void Start()
    {
        
    }
#if UNITY_EDITOR
    [ContextMenu(nameof(TestSpawnEnemy))]
    private void TestSpawnEnemy()
    {
        SpawnEnemy(0, Vector3.zero);
    }
#endif
    public void SpawnEnemy(int type, Vector3 position)
    {
        var newEnemy = EnemiesPool[type].Take();
        newEnemy.transform.position = position;
        newEnemy.NavAgent.enabled = true;
        newEnemy.gameObject.SetActive(true);
        newEnemy.OnDie.AddListener(() => DespawnEnemy(type, newEnemy));
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
        EnemiesPool[type].BackToQueue(instance);
    }
    private void OnCreateEnemy(int type, Enemy instance)
    {
        instance.gameObject.SetActive(false);
        instance.NavAgent.enabled = false;
    }
}
