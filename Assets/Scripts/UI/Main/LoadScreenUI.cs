using UnityEngine;

public class LoadScreenUI : BaseUI
{
    [SerializeField] private FillBarUI fillBarUI;
    public void Init()
    {
        gameObject.SetActive(false);
    }
    public void SetProgress(float progress)
    {
        fillBarUI.Set((int)(progress * 100), 100);
    }
}
