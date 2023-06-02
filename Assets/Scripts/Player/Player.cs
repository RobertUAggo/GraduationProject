using System.Collections;
using UnityEngine;

public class Player : BaseCreature
{
    public static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    private static readonly int DeadParam = Animator.StringToHash("Dead");
    [SerializeField] private float healthRegenTime = 2f;
    [SerializeField] private Vector2 handAttackRange = new Vector2(2,2);
    [SerializeField] private ParticleSystem handParticle;
    [SerializeField] private float rangeAttackRate = 1f;
    [SerializeField] private Transform[] rangeAttackTransforms;
    [SerializeField] private AnimationCurve maxExpPerLevel;
    [SerializeField] private AnimationCurve baseHealthPerLevel;
    [SerializeField] private AnimationCurve baseDamagePerLevel;
    [SerializeField] private BulletsManager bulletsManager;
    [SerializeField] private FillBarUI healthBarUI;
    [SerializeField] private FillBarUI expBarUI;
    public SkinManager SkinManager;
    public Animator Animator;
    private CapsuleCollider _capsuleCollider;
    private int _maxExp;
    private int _baseHealthLevel = 1;
    private int _baseDamageLevel = 1;
    private int _plusHealthPercent = 0;
    private int _plusDamagePercent = 0;
    public float Exp { private set; get; }
    private void Awake()
    {
        SkinManager.SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        SkinManager.CurrentSkin.OnStart.Invoke();
        MaxHealth = Mathf.RoundToInt(baseHealthPerLevel.Evaluate(_baseHealthLevel) * (1 + (_plusHealthPercent / 100f)));
        Health = MaxHealth;
        //Damage = baseDamage + Mathf.RoundToInt(baseDamage * (_plusDamagePercent / 100f));
        Damage = Mathf.RoundToInt(baseDamagePerLevel.Evaluate(_baseDamageLevel) * (1 + (_plusDamagePercent / 100f)));
    }
    private void Start()
    {

    }
    public void ControllerStart()
    {
        Animator.SetLayerWeight(1, 1);
        _maxExp = (int)maxExpPerLevel.Evaluate(CurrentLevel);
        expBarUI.Set(Exp, _maxExp);
        bulletsManager.Init();
        healthBarUI.Set(Health, MaxHealth);
        Level.Instance.LevelUI.PlayerLevelTextField.text = $"LVL {CurrentLevel}";
        Level.Instance.LevelUI.PlayerDamageTextField.text = Damage.ToString();
        StartCoroutine(C_RangeAttack());
        StartCoroutine(C_HealthRegen());
    }
#if UNITY_EDITOR
    [ContextMenu(nameof(TestTakeDamage))]
    private void TestTakeDamage()
    {
        TakeDamage(10);
    }
#endif
    private void AfterTakeDamage(float damage)
    {
        healthBarUI.Set(Health, MaxHealth);
        Level.Instance.FloatingDamage.Show($"-{damage}", 
            transform.position + transform.up * 3);
    }
    private void AfterDie()
    {
        Animator.SetLayerWeight(1, 0);
        healthBarUI.Set(Health, MaxHealth);
        Level.Instance.EndLevel();
        Animator.SetTrigger(DeadParam);
    }
    private IEnumerator C_HealthRegen()
    {
        while (IsAlive)
        {
            yield return new WaitForSeconds(healthRegenTime);
            if (Health < MaxHealth)
            {
                Health += 1;
                healthBarUI.Set(Health, MaxHealth);
            }
        }
    }
    private IEnumerator C_RangeAttack()
    {
        yield return new WaitForSeconds(rangeAttackRate);
        while (IsAlive)
        {
            for (int i = 0; i < rangeAttackTransforms.Length; i++)
            {
                bulletsManager.Shoot(this, 
                    Mathf.RoundToInt(Damage / 2f), 
                    rangeAttackTransforms[i].position,
                    rangeAttackTransforms[i].forward);
            }
            yield return new WaitForSeconds(rangeAttackRate);
        }
    }
    public void TakeExp(int addExp)
    {
        Exp += addExp;
        if (Exp >= _maxExp) LevelUp();
        expBarUI.Set(Exp, _maxExp);
    }
    [ContextMenu(nameof(LevelUp))]
    public void LevelUp()
    {
        Level.Instance.LevelUI.ChooseUpgradeUI.Show();
        CurrentLevel += 1;
        Level.Instance.LevelUI.PlayerLevelTextField.text = $"LVL {CurrentLevel}";
        Exp -= _maxExp;
        _maxExp = (int)maxExpPerLevel.Evaluate(CurrentLevel);
    }
    public void AddBaseHealth()
    {
        _baseHealthLevel++;
        MaxHealth = Mathf.RoundToInt(baseHealthPerLevel.Evaluate(_baseHealthLevel) * (1 + (_plusHealthPercent / 100f)));
        healthBarUI.Set(Health, MaxHealth);
    }
    public void AddBaseDamage()
    {
        _baseDamageLevel++;
        Damage = Mathf.RoundToInt(baseDamagePerLevel.Evaluate(_baseDamageLevel) * (1 + (_plusDamagePercent / 100f)));
        Level.Instance.LevelUI.PlayerDamageTextField.text = Damage.ToString();
    }
    public void AddHealthPercent(int addPercent)
    {
        _plusHealthPercent += addPercent;
    }
    public void AddDamagePercent(int addPercent)
    {
        _plusDamagePercent += addPercent;
    }
    public void HandAttack()
    {
        handParticle.Play();
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * handAttackRange.y,
            new Vector3(handAttackRange.x, 2, handAttackRange.y),
            transform.rotation, 
            1 << LayersManager.Enemy, 
            QueryTriggerInteraction.Collide);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Bullet")) continue;
            //Debug.Log($"collider: {collider.gameObject.name}");
            Vector3 toCollider = collider.transform.position - transform.position;
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, toCollider);
            if (Physics.Raycast(ray, 
                toCollider.magnitude, 
                1 << LayersManager.EnvironmentObject,
                QueryTriggerInteraction.Collide) == false)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(Damage);
                Debug.DrawRay(ray.origin, toCollider, Color.green, 2f);
            }
            else
            {
                Debug.DrawRay(ray.origin, toCollider, Color.red, 2f);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + transform.forward * handAttackRange.y,
            new Vector3(handAttackRange.x, 2, handAttackRange.y) * 2);
        for (int i = 0; i < rangeAttackTransforms.Length; i++)
        {
            Gizmos.DrawLine(rangeAttackTransforms[i].position,
                rangeAttackTransforms[i].position + rangeAttackTransforms[i].forward * 3);
        }
    }
}
