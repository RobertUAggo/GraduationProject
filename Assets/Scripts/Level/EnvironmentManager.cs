using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    public readonly List<EnvironmentObject> CurrentObjects = new List<EnvironmentObject>();
    public void LoadData(LevelSaveData levelData)
    {
        EnvironmentObject newObject;
        foreach (var objData in levelData.EnvironmentObjects)
        {
            newObject = Instantiate(Main.Instance.ItemsManager.EnvironmentObjects[objData.ObjectId],
                new Vector3(objData.Position.x, transform.position.y, objData.Position.y), 
                Quaternion.Euler(0, objData.RotationY, 0), transform);
        }
        navMeshSurface.BuildNavMesh();
    }
}
