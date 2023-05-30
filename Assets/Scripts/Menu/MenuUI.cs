using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public ShopUI ShopUI;
    public SettingsUI SettingsUI;
    public void Init()
    {
        SettingsUI.Init();
        ShopUI.Init();
    }
    public void PlayClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Level);
    }
    public void CreatorClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Creator);
    }
}
