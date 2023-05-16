using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    public readonly List<EnvironmentObject> CurrentObjects = new List<EnvironmentObject>();
    public void Load(LevelSaveData levelSaveData)
    {
        EnvironmentObject newObject;
        foreach (var objData in levelSaveData.EnvironmentObjects)
        {
            newObject = Instantiate(Main.Instance.ItemsManager.EnvironmentObjects[objData.ObjectId],
                new Vector3(objData.Position.x, transform.position.y, objData.Position.y), 
                Quaternion.Euler(0, objData.RotationY, 0), transform);
        }
        navMeshSurface.BuildNavMesh();
    }
    public void Save(string path)
    {
        LevelSaveData levelSaveData = new LevelSaveData();
        levelSaveData.EnvironmentObjects = new List<EnvironmentObjectSaveData>(CurrentObjects.Count);
        for (int i = 0; i < levelSaveData.EnvironmentObjects.Count; i++)
        {
            levelSaveData.EnvironmentObjects[i] = new EnvironmentObjectSaveData()
            {
                ObjectId = CurrentObjects[i].ObjectId,
                Position = new Vector2(CurrentObjects[i].transform.position.x, CurrentObjects[i].transform.position.z),
                RotationY = CurrentObjects[i].transform.rotation.eulerAngles.y,
            };
        }
        SaveLoadSystem.Save(levelSaveData, path, SerializeType.JSON);
    }
}
