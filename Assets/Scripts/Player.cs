using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Serializable]
    private class SkinObjects
    {
        public GameObject[] GameObjects;
    }
    [SerializeField] private SkinObjects[] skins;
    private void Start()
    {
        SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);
    }
    public void SetSkin(int skinId)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            for (int j = 0; j < skins[i].GameObjects.Length; j++)
            {
                skins[i].GameObjects[j].SetActive(false);
            }
        }
        for (int j = 0; j < skins[skinId].GameObjects.Length; j++)
        {
            skins[skinId].GameObjects[j].SetActive(true);
        }
        SkinData data = Main.Instance.ItemsManager.Skinds[skinId];
        data.Effect.Invoke(this);
    }
}
