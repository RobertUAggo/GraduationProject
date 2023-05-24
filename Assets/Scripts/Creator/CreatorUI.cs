using TMPro;
using UnityEngine;

public enum CreatorMode
{
    Camera,
    Position,
    Rotation,
}

public class CreatorUI : MonoBehaviour
{
    [SerializeField] private CreatorInputUI creatorInputUI;
    [SerializeField] private TextMeshProUGUI switchModeTextField;
    public ChoosePanelUI ChoosePanelUI;
    public void Init()
    {
        ChoosePanelUI.Init();
        SwitchModeClick();
        SetMode(CreatorMode.Camera);
    }
    public void SwitchModeClick()
    {
        Creator.Instance.Mode++;
        if(Creator.Instance.Mode > CreatorMode.Rotation)
        {
            Creator.Instance.Mode = 0;
        }
        SetMode(Creator.Instance.Mode);
    }
    private void SetMode(CreatorMode mode)
    {
        Creator.Instance.Mode = mode;
        switchModeTextField.text = Creator.Instance.Mode.ToString();
        creatorInputUI.gameObject.SetActive(mode == CreatorMode.Camera);
    }
    public void BackToMenuClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Menu);
    }
    public void SaveClick()
    {
        Creator.Instance.EnvironmentManager.Save();
    }
    public void SaveAsClick()
    {

    }
}
