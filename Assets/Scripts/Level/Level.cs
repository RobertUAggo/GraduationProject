using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public LevelUI LevelUI;
    public PlayerController PlayerController;
    public EnvironmentManager EnvironmentManager;
    private void Awake()
    {
        SingletonInit();
        EnvironmentManager.Init();
        LevelUI.Init();
        PlayerController.Init();
#if UNITY_EDITOR
        Main.Instance.SceneLoader.EditorSetCurrentScene(GameScene.Level);
#endif
    }

}
