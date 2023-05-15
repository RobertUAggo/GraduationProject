using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(SkinData))]
public class SkinData : ItemData
{
    public UnityEvent<Player> Effect = new UnityEvent<Player>();
}
