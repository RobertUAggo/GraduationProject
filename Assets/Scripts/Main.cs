using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { private set; get; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of Main already exists!");
        }
        Instance = this;
    }
}
