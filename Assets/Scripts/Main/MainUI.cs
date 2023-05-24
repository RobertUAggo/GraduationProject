using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public LoadScreenUI LoadScreenUI;
    public Canvas Canvas { get; private set; }
    public void Init()
    {
        Canvas = GetComponent<Canvas>();
        LoadScreenUI.Init();
    }
}
