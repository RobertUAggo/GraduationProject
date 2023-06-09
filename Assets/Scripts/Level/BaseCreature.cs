using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCreature : MonoBehaviour
{
    public readonly UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public readonly UnityEvent OnDie = new UnityEvent();
    public int CurrentLevel { get; set; } = 1;
    public int MaxHealth { get; protected set; }
    public int Health { get; protected set; }
    public int Damage { get; protected set; }
    public bool IsAlive => Health != 0;
    public void TakeDamage(int damage)
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
