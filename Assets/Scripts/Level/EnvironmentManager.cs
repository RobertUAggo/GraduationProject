using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    private readonly List<EnvironmentObject> _currentObjects = new List<EnvironmentObject>();
    public Plane Plane { private set; get; }
    public void Init()
    {
        Plane = new Plane(transform.up, transform.position.y);
        if (Main.Instance.SceneLoader.LevelDataFilePath.Length != 0)
        {
            try
            {
                Load(Main.Instance.SceneLoader.LevelDataFilePath);
            }
            catch
            {
                Main.Instance.SceneLoader.LevelDataFilePath = "";
                Debug.LogError($"Trying to load bad file");
            }
        }
    }
    public EnvironmentObject AddObject(int objectId, float positionX, float positionZ, float rotationY)
    {
        Vector3 resultPos = new Vector3(positionX, 0, positionZ);
        Quaternion resultRot = Quaternion.Euler(0, rotationY, 0);
        EnvironmentObject newObject = Instantiate(Main.Instance.ItemsManager.EnvironmentObjects[objectId],
            resultPos,
            resultRot,
            transform);
        newObject.ObjectId = objectId;
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
        LevelSaveData levelSaveData = SaveLoadSystem.Load<LevelSaveData>(path);
        Load(levelSaveData);
        Debug.Log($"Load: {path} (Objects: {_currentObjects.Count})");
    }
    public void Load(LevelSaveData levelSaveData)
    {
        foreach (var objData in levelSaveData.EnvironmentObjects)
        {
            AddObject(objData.ObjectId, objData.PositionX, objData.PositionZ, objData.RotationY);
        }
    }
    public void BuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
        foreach (var obj in _currentObjects)
        {
            obj.SetColliderAsTrigger();
        }
    }
    public void Save(string path)
    {
        Debug.Log($"Save: {path} (Objects: {_currentObjects.Count})");
        Main.Instance.SceneLoader.LevelDataFilePath = path;
        LevelSaveData levelSaveData = new LevelSaveData();
        levelSaveData.EnvironmentObjects = new EnvironmentObjectSaveData[_currentObjects.Count];
        for (int i = 0; i < levelSaveData.EnvironmentObjects.Length; i++)
        {
            levelSaveData.EnvironmentObjects[i] = new EnvironmentObjectSaveData()
            {
                ObjectId = _currentObjects[i].ObjectId,
                PositionX = _currentObjects[i].transform.position.x,
                PositionZ = _currentObjects[i].transform.position.z,
                RotationY = _currentObjects[i].transform.rotation.eulerAngles.y,
            };
        }
        SaveLoadSystem.Save(levelSaveData, path);
    }
    public void Save()
    {
        Save(Main.Instance.SceneLoader.LevelDataFilePath);
    }
}
