using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenUI : BaseUI
{
    [SerializeField] private FillBarUI fillBarUI;
    public void Init()
    {
        gameObject.SetActive(false);
    }
    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetProgress(float progress)
    {
        fillBarUI.Set((int)(progress * 100), 100);
    }

}
