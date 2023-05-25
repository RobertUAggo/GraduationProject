using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public EndUI EndUI;
    public void Init()
    {
        
    }
    public void BackToMenuClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Menu);
    }
}
