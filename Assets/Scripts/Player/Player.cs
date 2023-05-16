using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FillBarUI healthBarUI;
    public SkinManager SkinManager;
    public readonly HealthController HealthController = new HealthController();
    private void Start()
    {
        HealthController.OnTakeDamage.AddListener(OnTakeDamage);
        HealthController.OnDie.AddListener(OnDie);
        SkinManager.SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);

        HealthController.Init(100); //TODO
    }
    private void OnTakeDamage(float damage)
    {
        healthBarUI.Set(HealthController.Health, HealthController.MaxHealth);
    }
    private void OnDie()
    {

    }
    
}
