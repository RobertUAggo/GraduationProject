using UnityEngine;

public class Creator : MonoSingleton<Creator>
{
    public CreatorUI CreatorUI;
    public EnvironmentManager EnvironmentManager;
    //
    private void Awake()
    {
        SingletonInit();
    }
}
