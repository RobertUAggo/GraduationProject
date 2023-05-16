using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public LevelUI LevelUI;
    public PlayerController PlayerController;
    public EnvironmentManager EnvironmentManager;
    private void Awake()
    {
        SingletonInit();
        LevelUI.Init();
        PlayerController.Init();
    }

}
