using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    private readonly List<EnvironmentObject> _currentObjects = new List<EnvironmentObject>();
    private void Awake()
    {
#if UNITY_EDITOR
        if(Main.Instance.SceneLoader.LevelDataFilePath.Length == 0)
        {
            Main.Instance.SceneLoader.LevelDataFilePath = 
                Application.persistentDataPath + "\\" + "default.json";
        }
#endif
        Load(Main.Instance.SceneLoader.LevelDataFilePath);
    }
    public EnvironmentObject AddObject(int objectId, Vector2 position, float rotationY)
    {
        Vector3 resultPos = new Vector3(position.x, 0, position.y);
        Quaternion resultRot = Quaternion.Euler(0, rotationY, 0);
        EnvironmentObject newObject = Instantiate(Main.Instance.ItemsManager.EnvironmentObjects[objectId],
            resultPos,
            resultRot,
            transform);
        _currentObjects.Add(newObject);
        return newObject;
    }
    public void RemoveObject(EnvironmentObject removingObject)
    {
        _currentObjects.Remove(removingObject);
        Destroy(removingObject.gameObject);
    }
    public void Load(string path)
    {
        Main.Instance.SceneLoader.LevelDataFilePath = path;
        LevelSaveData levelSaveData = SaveLoadSystem.Load<LevelSaveData>(path, SerializeType.JSON);
        Load(levelSaveData);
    }
    public void Load(LevelSaveData levelSaveData)
    {
        foreach (var objData in levelSaveData.EnvironmentObjects)
        {
            AddObject(objData.ObjectId, objData.Position, objData.RotationY);
        }
        navMeshSurface.BuildNavMesh();
    }
    public void Save(string path)
    {
        Main.Instance.SceneLoader.LevelDataFilePath = path;
        LevelSaveData levelSaveData = new LevelSaveData();
        levelSaveData.EnvironmentObjects = new List<EnvironmentObjectSaveData>(_currentObjects.Count);
        for (int i = 0; i < levelSaveData.EnvironmentObjects.Count; i++)
        {
            levelSaveData.EnvironmentObjects[i] = new EnvironmentObjectSaveData()
            {
                ObjectId = _currentObjects[i].ObjectId,
                Position = new Vector2(_currentObjects[i].transform.position.x, _currentObjects[i].transform.position.z),
                RotationY = _currentObjects[i].transform.rotation.eulerAngles.y,
            };
        }
        SaveLoadSystem.Save(levelSaveData, path, SerializeType.JSON);
    }
    public void Save()
    {
        Save(Main.Instance.SceneLoader.LevelDataFilePath);
    }
}
