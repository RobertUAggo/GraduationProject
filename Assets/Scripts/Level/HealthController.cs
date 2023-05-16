using UnityEngine.Events;

public class HealthController
{
    public readonly UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public readonly UnityEvent OnDie = new UnityEvent();
    public float MaxHealth { get; protected set; }
    public float Health { get; protected set; }
    public bool IsAlive => Health != 0;
    public void Init(float maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (Health == 0) return;
        Health -= damage;
        OnTakeDamage.Invoke(damage);
        if (Health < 0) Die();
    }
    public void Die()
    {
        Health = 0;
        OnDie.Invoke();
    }
}
