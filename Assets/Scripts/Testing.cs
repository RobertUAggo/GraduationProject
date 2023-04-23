using System;
using System.IO;
using UnityEngine;

[Serializable]
public class TestData
{
    public string name = "TestName";
    public float[] values = new float[] { 1, 2, 3 };
    public override string ToString()
    {
        return $"{name} - {string.Join(", ", values)}";
    }
}

public class Testing : MonoBehaviour
{
    [SerializeField] private SerializeType serializeType;
    [SerializeField] private TestData[] testDataArray;
    private string path;
    //private SaveLoadData<int[]> TestFile = new SaveLoadData<int[]>();
    //private int[][] testField2 = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
    private void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "test");
        switch (serializeType)
        {
            case SerializeType.JSON:
                path += ".json";
                break;
            case SerializeType.XML:
                path += ".xml";
                break;
        }
    }

    [ContextMenu(nameof(TestSave))]
    private void TestSave()
    {
        SetPath();
        CheckTime($"{serializeType} SaveTime", () =>
        {
            SaveLoadSystem.Save(testDataArray, path, serializeType);
        });
    }
    [ContextMenu(nameof(TestLoad))]
    private void TestLoad()
    {
        SetPath();
        CheckTime($"{serializeType} LoadTime", () =>
        {
            testDataArray = SaveLoadSystem.Load<TestData[]>(path, serializeType);
        });
    }

#if UNITY_EDITOR

    [ContextMenu(nameof(LogPath))]
    private void LogPath()
    {
        SetPath();
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
