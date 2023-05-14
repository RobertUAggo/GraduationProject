using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoSingleton<Creator>
{
    public CreatorUI CreatorUI;
    private void Awake()
    {
        SingletonInit();
    }
}
