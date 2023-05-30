using UnityEngine;

public class ChooseUpgradeUI : BaseUI
{
    public override void Show()
    {
        base.Show();
        Time.timeScale = 0;
    }
    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }
}
