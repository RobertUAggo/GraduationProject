using System;
using UnityEngine;

[Serializable]
public class SkinManager
{
    [SerializeField] private SkinData[] skins;
    public void SetSkin(int skinId)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            for (int j = 0; j < skins[i].GameObjects.Length; j++)
            {
                skins[i].GameObjects[j].SetActive(false);
            }
        }
        var currentSkinData = skins[skinId];
        for (int j = 0; j < currentSkinData.GameObjects.Length; j++)
        {
            currentSkinData.GameObjects[j].SetActive(true);
        }
        currentSkinData.Effect.Invoke();
    }
}