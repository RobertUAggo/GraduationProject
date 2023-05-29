using UnityEngine;

public class Player : BaseCreature
{
    public static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    private static readonly int DeadParam = Animator.StringToHash("Dead");
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private AnimationCurve maxExpPerLevel;
    [SerializeField] private FillBarUI healthBarUI;
    [SerializeField] private FillBarUI expBarUI;
    public SkinManager SkinManager;
    public Animator Animator;
    private int _maxExp;
    private int _plusHealthPercent = 0;
    private int _plusDamagePercent = 0;
    public float Exp { private set; get; }
    private void Awake()
    {
        SkinManager.SetSkin(Main.Instance.PlayerManager.PlayerData.CurrentSkin);
        OnTakeDamage.AddListener(AfterTakeDamage);
        OnDie.AddListener(AfterDie);
        //
        CurrentLevel = 0;
        _maxExp = (int)maxExpPerLevel.Evaluate(CurrentLevel);
        expBarUI.Set(Exp, _maxExp);
    }
    private void Start()
    {
        SkinManager.CurrentSkin.OnStart.Invoke();
        MaxHealth = baseHealth + Mathf.RoundToInt(baseHealth * (_plusHealthPercent / 100f));
        Health = MaxHealth;
        Damage = baseDamage + Mathf.RoundToInt(baseDamage * (_plusDamagePercent / 100f));
        healthBarUI.Set(Health, MaxHealth);
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
            transform.position + transform.up * 2);
    }
    private void AfterDie()
    {
        healthBarUI.gameObject.SetActive(false);
        Level.Instance.EndLevel();
        Animator.SetTrigger(DeadParam);
    }
    public void TakeExp(int addExp)
    {
        Exp += addExp;
        if (Exp >= _maxExp) LevelUp();
        expBarUI.Set(Exp, _maxExp);
    }
    public void LevelUp()
    {
        CurrentLevel += 1;
        Exp -= _maxExp;
        _maxExp = (int)maxExpPerLevel.Evaluate(CurrentLevel);
        MaxHealth = baseHealth + Mathf.RoundToInt(baseHealth * (_plusHealthPercent / 100f));
        Damage = baseDamage + Mathf.RoundToInt(baseDamage * (_plusDamagePercent / 100f));
    }
    public void AddBaseHealth(int addHealth)
    {
        baseHealth += addHealth;
    }
    public void AddBaseDamage(int addDamage)
    {
        baseDamage += addDamage;
    }
    public void AddHealthPercent(int addPercent)
    {
        _plusHealthPercent += addPercent;
    }
    public void AddDamagePercent(int addPercent)
    {
        _plusDamagePercent += addPercent;
    }
}
