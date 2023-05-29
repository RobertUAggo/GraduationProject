using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : BaseCreature
{
    private static int AttackParam = Animator.StringToHash("Attack");
    private static int DeadParam = Animator.StringToHash("Dead");
    private static int ResetParam = Animator.StringToHash("Reset");
    //
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent<BaseCreature> OnAttack;
    public NavMeshAgent NavAgent { private set; get; }
    private float _attackDistanceSqr;
    private void Awake()
    {
        _attackDistanceSqr = attackDistance * attackDistance;
        NavAgent = GetComponent<NavMeshAgent>();
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        MaxHealth = 100; //TODO
        Health = MaxHealth;
    }
    private void OnEnable()
    {
        animator.SetTrigger(ResetParam);
        StartCoroutine(C_AfterEnable());
    }
    private IEnumerator C_AfterEnable()
    {
        yield return null;
        StartChase(Level.Instance.PlayerController.Player);
    }
    private void AfterTakeDamage(float damage)
    {
        Level.Instance.FloatingDamage.Show($"-{damage}",
            transform.position + transform.up * 1);
    }
    private void AfterDie()
    {
        animator.SetTrigger(DeadParam);
    }
    public void RotateTowards(BaseCreature target)
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
    public void StartChase(BaseCreature target)
    {
        StopAllCoroutines();
        StartCoroutine(C_ChaseLogic(target));
        StartCoroutine(C_AttackLogic(target));
    }
    private IEnumerator C_ChaseLogic(BaseCreature target)
    {
        yield return null;
        while (IsAlive && target != null)
        {
            NavAgent.SetDestination(target.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }
    private IEnumerator C_AttackLogic(BaseCreature target)
    {
        yield return null;
        while (IsAlive && target != null && target.IsAlive)
        {
            if ((transform.position - target.transform.position).sqrMagnitude < _attackDistanceSqr)
            {
                OnAttack.Invoke(target);
                animator.SetTrigger(AttackParam);
                Level.Instance.PlayerController.Player.TakeDamage(Damage);
                yield return new WaitForSeconds(attackRate);
            }
            else
            {
                animator.ResetTrigger(AttackParam);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
