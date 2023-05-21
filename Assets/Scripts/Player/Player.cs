using System;
using UnityEngine;

public class Player : BaseCreature
{
    [SerializeField] private FillBarUI healthBarUI;
    public SkinManager SkinManager;
    private void Awake()
    {
        SkinManager.SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        MaxHealth = 100; //TODO
        Health = MaxHealth;
    }

    private void AfterTakeDamage(float damage)
    {
        healthBarUI.Set(Health, MaxHealth);
    }
    private void AfterDie()
    {

    }
    
}