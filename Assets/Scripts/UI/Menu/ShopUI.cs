using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : BaseUI
{
    [SerializeField] private BuySkinUI[] buySkinsUI;
    public void Init()
    {
        for (int i = 0; i < buySkinsUI.Length; i++)
        {
            buySkinsUI[i].Set(i);
        }
    }
    public void EquipSkin(int skinId)
    {
        buySkinsUI[Main.Instance.PlayerManager.PlayerData.CurrentSkin].SetEquip(false);
        Main.Instance.PlayerManager.PlayerData.CurrentSkin = skinId;
        Main.Instance.PlayerManager.Save();
        buySkinsUI[Main.Instance.PlayerManager.PlayerData.CurrentSkin].SetEquip(true);
    }
}
