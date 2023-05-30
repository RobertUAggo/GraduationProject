public enum UpgradeType
{
    Health,
    Damage,
    AttackRate,
    Speed,
}
public struct PlayerData
{
    public float Record;
    public int Money;
    public int CurrentSkin;
    public bool[] OwnedSkins;
    public int[] Upgrades;
    public int MaxFPS;
}
