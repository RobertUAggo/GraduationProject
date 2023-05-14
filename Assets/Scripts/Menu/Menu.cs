using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoSingleton<Menu>
{
    public MenuUI MenuUI;
    private void Awake()
    {
        SingletonInit();
    }
}
