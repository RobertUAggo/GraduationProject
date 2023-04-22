using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private SerializeType serializeType;
    private string path;
    //private SaveLoadData<int[]> TestFile = new SaveLoadData<int[]>();
    [SerializeField] private int[] testField = new int[] {1,2,3};
    //private int[][] testField2 = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "test");
    }

    [ContextMenu(nameof(TestSave))]
    private void TestSave()
    {
        CheckTime($"{serializeType} SaveTime", () =>
        {
            SaveLoadSystem.Save(testField, path, serializeType);
        });
        Debug.Log($"Saved = [{string.Join(", ", testField)}]");
    }
    [ContextMenu(nameof(TestLoad))]
    private void TestLoad()
    {
        CheckTime($"{serializeType} LoadTime", () =>
        {
            testField = SaveLoadSystem.Load<int[]>(path, serializeType);
        });
        Debug.Log($"Loaded = [{string.Join(", ", testField)}]");
    }

#if UNITY_EDITOR

    [ContextMenu(nameof(LogPath))]
    private void LogPath()
    {
        Debug.Log($"path = {path}");
    }
#endif
    public static void CheckTime(string label, Action act)
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        act();
        sw.Stop();
        Debug.Log($"{label}: {sw.Elapsed}");
    }
}
