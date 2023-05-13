using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    private void Awake()
    {
        SingletonInit();
    }
}
