using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Serializable]
    private class SkinObjects
    {
        public GameObject[] GameObjects;
    }
    [SerializeField] private SkinObjects[] skins;

    public readonly HealthController HealthController = new HealthController();
    private void Start()
    {
        HealthController.OnTakeDamage.AddListener(OnTakeDamage);
        HealthController.OnDie.AddListener(OnDie);
        SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);
    }
    private void OnTakeDamage(float damage)
    {

    }
    private void OnDie()
    {

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
        SkinData data = Main.Instance.ItemsManager.Skins[skinId];
        data.Effect.Invoke(this);
    }
}
