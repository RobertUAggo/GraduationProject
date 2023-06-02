using UnityEngine;

public class ChooseUpgradeUI : BaseUI
{
    public override void Show()
    {
        base.Show();
        Level.Instance.PlayerController.Joystick.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public override void Hide()
    {
        base.Hide();
        Level.Instance.PlayerController.Joystick.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
