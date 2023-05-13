using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoSingleton<Menu>
{
    private void Awake()
    {
        SingletonInit();
    }
}
