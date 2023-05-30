using System;

[Serializable]
public class PlayerData
{
    public float Record;
    public int Money;
    public int CurrentSkin;
    public bool[] OwnedSkins = new bool[] {true, false, false};
    public int MaxFPS = 30;
}
