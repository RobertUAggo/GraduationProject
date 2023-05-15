using UnityEngine.Events;

public class SkinData : ItemData
{
    public UnityEvent<Player> Effect = new UnityEvent<Player>();
}
