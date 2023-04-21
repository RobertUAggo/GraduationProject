using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public SaveFile<int[]> TestSaveFile;

    [SerializeField] private int[] testField = new int[] {1,2,3};

    private void Awake()
    {
        TestSaveFile = new SaveFile<int[]>("Test");
    }
    [ContextMenu(nameof(TestSave))]
    private void TestSave()
    {
        TestSaveFile.Save(testField);
    }
    [ContextMenu(nameof(TestLoad))]
    private void TestLoad()
    {
        testField = TestSaveFile.Load();
        Debug.Log($"testField = [{string.Join(", ", testField)}]");
    }
}
