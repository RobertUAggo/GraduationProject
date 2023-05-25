using System;
using UnityEngine;

public class Player : BaseCreature
{
    public static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    private static readonly int DeadParam = Animator.StringToHash("Dead");
    [SerializeField] private FillBarUI healthBarUI;
    public SkinManager SkinManager;
    public Animator Animator;
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
        healthBarUI.gameObject.SetActive(false);
        Level.Instance.EndLevel();
        Animator.SetTrigger(DeadParam);
    }
    
}
