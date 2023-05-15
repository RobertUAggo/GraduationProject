using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public Joystick Joystick;
    public void Init()
    {

    }
    public void BackToMenuClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Menu);
    }
}
