using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : BaseCreature
{
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private UnityEvent<Transform> Attack;
    private NavMeshAgent _navAgent;
    private float _attackDistanceSqr;
    private void Awake()
    {
        _attackDistanceSqr = attackDistance * attackDistance;
        _navAgent = GetComponent<NavMeshAgent>();
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        MaxHealth = 100; //TODO
        Health = MaxHealth;
    }
    private void Start()
    {
        StartChase(Level.Instance.PlayerController.transform);
    }
    private void AfterTakeDamage(float damage)
    {

    }
    private void AfterDie()
    {

    }
    public void StartChase(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(C_ChaseLogic(target));
        StartCoroutine(C_AttackLogic(target));
    }
    private IEnumerator C_ChaseLogic(Transform target)
    {
        yield return null;
        while (IsAlive && target != null)
        {
            _navAgent.SetDestination(target.position);
            yield return new WaitForFixedUpdate();
        }
    }
    private IEnumerator C_AttackLogic(Transform target)
    {
        yield return null;
        while (IsAlive && target != null)
        {
            if ((transform.position - target.position).sqrMagnitude < _attackDistanceSqr)
            {
                Attack.Invoke(target);
                yield return new WaitForSeconds(attackRate);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
