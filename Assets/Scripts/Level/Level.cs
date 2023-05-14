using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public LevelUI LevelUI;
    private void Awake()
    {
        SingletonInit();
    }
}
