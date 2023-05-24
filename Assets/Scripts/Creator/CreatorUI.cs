using TMPro;
using UnityEngine;

public class CreatorUI : MonoBehaviour
{
    [SerializeField] private CreatorInputUI creatorInputUI;
    [SerializeField] private TextMeshProUGUI switchModeTextField;
    public ChoosePanelUI ChoosePanelUI;
    private bool _cameraMode = false;
    public void Init()
    {
        ChoosePanelUI.Init();
        SwitchModeClick();
    }
    public void SwitchModeClick()
    {
        _cameraMode = !_cameraMode;
        switchModeTextField.text = _cameraMode ? "CAMERA" : "MOVING";
        creatorInputUI.gameObject.SetActive(_cameraMode);
    }
    public void SaveClick()
    {
        Creator.Instance.EnvironmentManager.Save();
    }
    public void SaveAsClick()
    {

    }
}
