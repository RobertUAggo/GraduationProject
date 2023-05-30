using UnityEngine;

public class Player : BaseCreature
{
    public static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    private static readonly int DeadParam = Animator.StringToHash("Dead");
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private AnimationCurve maxExpPerLevel;
    [SerializeField] private AnimationCurve baseHealthPerLevel;
    [SerializeField] private AnimationCurve baseDamagePerLevel;
    [SerializeField] private FillBarUI healthBarUI;
    [SerializeField] private FillBarUI expBarUI;
    public SkinManager SkinManager;
    public Animator Animator;
    private CapsuleCollider _capsuleCollider;
    private int _maxExp;
    private int _baseHealthLevel = 0;
    private int _baseDamageLevel = 0;
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
        Level.Instance.LevelUI.PlayerLevelTextField.text = $"LVL {CurrentLevel}";
        Level.Instance.LevelUI.PlayerDamageTextField.text = Damage.ToString();
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
        Level.Instance.LevelUI.ChooseUpgradeUI.Show();
        CurrentLevel += 1;
        Level.Instance.LevelUI.PlayerLevelTextField.text = $"LVL {CurrentLevel}";
        Exp -= _maxExp;
        _maxExp = (int)maxExpPerLevel.Evaluate(CurrentLevel);
    }
    public void AddBaseHealth()
    {
        _baseHealthLevel++;
        baseHealth = (int)baseHealthPerLevel.Evaluate(_baseHealthLevel);
        MaxHealth = baseHealth + Mathf.RoundToInt(baseHealth * (_plusHealthPercent / 100f));
        healthBarUI.Set(Health, MaxHealth);
    }
    public void AddBaseDamage()
    {
        _baseDamageLevel++;
        baseDamage = (int)baseDamagePerLevel.Evaluate(_baseHealthLevel);
        Damage = baseDamage + Mathf.RoundToInt(baseDamage * (_plusDamagePercent / 100f));
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
}
