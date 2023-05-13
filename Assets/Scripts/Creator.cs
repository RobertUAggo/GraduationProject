using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoSingleton<Creator>
{
    private void Awake()
    {
        SingletonInit();
    }
}
