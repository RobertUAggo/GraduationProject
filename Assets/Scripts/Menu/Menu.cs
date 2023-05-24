using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoSingleton<Menu>
{
    public MenuUI MenuUI;
    private void Awake()
    {
        SingletonInit();
        MenuUI.Init();
#if UNITY_EDITOR
        Main.Instance.SceneLoader.EditorSetCurrentScene(GameScene.Menu);
#endif
    }
}
