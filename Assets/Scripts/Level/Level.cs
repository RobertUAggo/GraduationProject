using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public PlayerController PlayerController;
    public LevelUI LevelUI;
    private void Awake()
    {
        SingletonInit();
        LevelUI.Init();
        PlayerController.Init();
    }
}
