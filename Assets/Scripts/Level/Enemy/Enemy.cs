using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseCreature
{
    private NavMeshAgent _navAgent;
    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        MaxHealth = 100; //TODO
        Health = MaxHealth;
    }
    private void AfterTakeDamage(float damage)
    {

    }
    private void AfterDie()
    {

    }
    public void StartChase(Player player)
    {
        _navAgent.SetDestination(player.transform.position);
    }
}
