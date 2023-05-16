using UnityEngine;

public enum EnviromentObjectType
{
    Wall,
    Visual,
}

public class EnvironmentObject : MonoBehaviour
{
    public string Name;
    public Sprite Sprite;
    public EnviromentObjectType EnviromentObjectType;
    
}