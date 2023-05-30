using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    [SerializeField] private Slider fpsSlider;
    [SerializeField] private TextMeshProUGUI fpsTextField;
    private Coroutine _saveCor = null;
    public void Init()
    {
        ChangeMaxFPS(Main.Instance.PlayerManager.PlayerData.MaxFPS);
        fpsSlider.SetValueWithoutNotify(Main.Instance.PlayerManager.PlayerData.MaxFPS);
    }
    public override void Hide()
    {
        base.Hide();
        if (_saveCor != null) Main.Instance.StopCoroutine(_saveCor);
        Main.Instance.PlayerManager.Save();
    }
    public void ChangeMaxFPS(float value)
    {
        int intValue = (int)value;
        Application.targetFrameRate = (int)value;
        Main.Instance.PlayerManager.PlayerData.MaxFPS = intValue;
        fpsTextField.text = intValue.ToString();
        SaveDelay();
    }
    private void SaveDelay()
    {
        if (_saveCor != null) Main.Instance.StopCoroutine(_saveCor);
        _saveCor = Main.Instance.StartCoroutine(C_SaveDelay());
    }
    private IEnumerator C_SaveDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        Main.Instance.PlayerManager.Save();
        _saveCor = null;
    }
}
