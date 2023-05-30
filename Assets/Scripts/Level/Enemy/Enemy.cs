using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : BaseCreature
{
    private static int IsMovingParam = Animator.StringToHash("IsMoving");
    private static int AttackParam = Animator.StringToHash("Attack");
    private static int DeadParam = Animator.StringToHash("Dead");
    private static int ResetParam = Animator.StringToHash("Reset");
    //
    [SerializeField] private AnimationCurve damagePerLevel;
    [SerializeField] private AnimationCurve healthPerLevel;
    [SerializeField] private AnimationCurve expPerLevel; 
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private Animator animator;
    public NavMeshAgent NavAgent { private set; get; }
    private float _attackDistanceSqr;
    private BaseCreature _target;
    private void Awake()
    {
        _attackDistanceSqr = attackDistance * attackDistance;
        NavAgent = GetComponent<NavMeshAgent>();
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
    }
    public void Init(int level)
    {
        CurrentLevel = level;
        MaxHealth = (int) healthPerLevel.Evaluate(CurrentLevel);
        Health = MaxHealth;
        Damage = (int)damagePerLevel.Evaluate(CurrentLevel);
        animator.SetTrigger(ResetParam);
        StartCoroutine(C_ChaseDelay());
    }
    private IEnumerator C_ChaseDelay()
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
        NavAgent.isStopped = true;
        animator.SetTrigger(DeadParam);
        Level.Instance.PlayerController.Player.TakeExp((int)expPerLevel.Evaluate(CurrentLevel));
    }
    public void RotateTowards(BaseCreature target)
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
    public void StartChase(BaseCreature target)
    {
        StopAllCoroutines();
        _target = target;
        StartCoroutine(C_ChaseLogic());
        StartCoroutine(C_AttackLogic());
    }
    private IEnumerator C_ChaseLogic()
    {
        yield return null;
        while (IsAlive && _target != null)
        {
            animator.SetBool(IsMovingParam, NavAgent.velocity != Vector3.zero);
            NavAgent.SetDestination(_target.transform.position);
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool(IsMovingParam, false);
    }
    private IEnumerator C_AttackLogic()
    {
        float time;
        Vector3 toTarget;
        yield return null;
        while (IsAlive && _target != null && _target.IsAlive)
        {
            toTarget = _target.transform.position - transform.position; 
            if (toTarget.sqrMagnitude < _attackDistanceSqr)
            {
                animator.SetTrigger(AttackParam);
                time = 0;
                while(time< attackRate)
                {
                    yield return null;
                    time += Time.deltaTime;
                    toTarget = _target.transform.position - transform.position;
                    transform.rotation = Quaternion.LookRotation(toTarget);
                }
                //yield return new WaitForSeconds(attackRate);
            }
            else
            {
                animator.ResetTrigger(AttackParam);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void ApplyDamageToTarget()
    {
        _target.TakeDamage(Damage);
    }
    public void RangeAttack()
    {
        Vector3 direction = _target.transform.position - transform.position;
        direction.y = 0;
        Level.Instance.BulletManager.Shoot(this, Damage, transform.position + Vector3.up, direction);
    }
}
