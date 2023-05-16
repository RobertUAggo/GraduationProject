using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public EnvironmentManager EnvironmentManager;
    public PlayerController PlayerController;
    public LevelUI LevelUI;
    private void Awake()
    {
        SingletonInit();
        LevelUI.Init();
        PlayerController.Init();
        EnvironmentManager.Init();
    }

}
