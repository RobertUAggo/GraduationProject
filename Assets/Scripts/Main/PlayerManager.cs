using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private const string c_saveFileName = "PlayerData";
    public PlayerData PlayerData;
    public readonly UnityEvent<int> OnMoneyChange = new UnityEvent<int>();
    private string _targetPath;
    public void Init()
    {
        _targetPath = Path.Combine(Application.persistentDataPath, c_saveFileName);
        PlayerData = SaveLoadSystem.Load<PlayerData>(_targetPath);
    }
    public void ChangeMoney(int value)
    {
        PlayerData.Money += value;
        OnMoneyChange.Invoke(PlayerData.Money);
        Debug.Log($"Money = {PlayerData.Money}");
    }
    [ContextMenu(nameof(Save))]
    public void Save()
    {
        SaveLoadSystem.Save(PlayerData, _targetPath);
    }
}
