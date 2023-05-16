using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    public List<EnvironmentObject> CurrentObjects = new List<EnvironmentObject>();
    public void Init()
    {

    }
    public void LoadData(LevelSaveData levelData)
    {
        EnvironmentObject newObject;
        foreach (var objData in levelData.EnvironmentObjects)
        {
            newObject = Instantiate(Main.Instance.ItemsManager.EnvironmentObjects[objData.ObjectId],
                objData.Position, Quaternion.Euler(0, objData.RotationY, 0), root);
        }
    }
}
