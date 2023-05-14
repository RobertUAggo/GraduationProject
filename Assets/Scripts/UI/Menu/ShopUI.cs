using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : BaseUI
{
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
}
