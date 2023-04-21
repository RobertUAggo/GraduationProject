using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public SaveFile TestSaveFile;

    [SerializeField] private int[] testField = new int[] {1,2,3};

    private void Awake()
    {
        TestSaveFile = new SaveFile("Test", new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter());
    }
    [ContextMenu(nameof(TestSave))]
    private void TestSave()
    {
        TestSaveFile.Save(testField);
    }
    [ContextMenu(nameof(TestLoad))]
    private void TestLoad()
    {
        testField = (int[]) TestSaveFile.Load();
        Debug.Log($"testField = [{string.Join(", ", testField)}]");
    }
}
