using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoSingleton<Main>
{
    private void Awake()
    {
        SingletonInit();
        //Debug.Log(Main.Instance, Main.Instance.gameObject);
    }
}
