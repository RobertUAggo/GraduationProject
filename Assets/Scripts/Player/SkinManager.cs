using System;
using UnityEngine;

[Serializable]
public class SkinManager
{
    [SerializeField] private SkinData[] skins;
    public SkinData CurrentSkin { private set; get; }
    public SkinData[] Skins => skins;
    public void SetSkin(int skinId)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            for (int j = 0; j < skins[i].GameObjects.Length; j++)
            {
                skins[i].GameObjects[j].SetActive(false);
            }
        }
        CurrentSkin = skins[skinId];
        for (int j = 0; j < CurrentSkin.GameObjects.Length; j++)
        {
            CurrentSkin.GameObjects[j].SetActive(true);
        }
    }
}