using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCreature : MonoBehaviour
{
    [SerializeField] private AnimationCurve damagePerLevel;
    [SerializeField] private AnimationCurve healthPerLevel;
    public readonly UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public readonly UnityEvent OnDie = new UnityEvent();
    public float MaxHealth { get; protected set; }
    public float Health { get; protected set; }
    public float Damage { get; protected set; }
    public bool IsAlive => Health != 0;

    public void SetLevel(int level)
    {
        MaxHealth = healthPerLevel.Evaluate(level);
        Health = MaxHealth;
        Damage = damagePerLevel.Evaluate(level);
    }
    public void TakeDamage(float damage)
    {
        if (Health == 0) return;
        //Debug.Log($"{name} OnTakeDamage({damage})", gameObject);
        Health -= damage;
        OnTakeDamage.Invoke(damage);
        if (Health <= 0) Die();
    }
    [ContextMenu(nameof(Die))]
    public void Die()
    {
        Health = 0;
        OnDie.Invoke();
    }
}
