using System;
using System.Linq;
using UnityEngine;

public class RootSpawner : BaseSpawner
{
    [Serializable]
    private class SpawnerWeight
    {
        public BaseSpawner Spawner;
        public float Weight;
    }
    [SerializeField] private SpawnerWeight[] childSpawnersWeights;
    public override Vector3 GetPoint()
    {
        float sum = childSpawnersWeights.Sum(x => x.Weight);
        float rand = UnityEngine.Random.value * sum;
        sum = 0;
        for (int i = 0; i < childSpawnersWeights.Length; i++)
        {
            sum += childSpawnersWeights[i].Weight;
            if (sum >= rand) return childSpawnersWeights[i].Spawner.GetPoint();
        }
        return childSpawnersWeights[childSpawnersWeights.Length - 1].Spawner.GetPoint();
    }
}