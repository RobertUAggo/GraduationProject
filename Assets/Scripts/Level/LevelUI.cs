using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public EndUI EndUI;
    public ChooseUpgradeUI ChooseUpgradeUI;
    public TextMeshProUGUI PlayerLevelTextField;
    public TextMeshProUGUI PlayerDamageTextField;
    public void Init()
    {

    }
    public void BackToMenuClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Menu);
    }
}
