using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public LevelUI LevelUI;
    public PlayerController PlayerController;
    public EnemiesManager EnemiesManager;
    public EnvironmentManager EnvironmentManager;
    public FloatingTextManager FloatingDamage;
    private void Awake()
    {
        SingletonInit();
        EnvironmentManager.Init();
        EnemiesManager.Init();
        PlayerController.Init();
        FloatingDamage.Init();
        LevelUI.Init();

#if UNITY_EDITOR
        Main.Instance.SceneLoader.EditorSetCurrentScene(GameScene.Level);
#endif
    }
    public void EndLevel()
    {
        LevelUI.EndUI.Show();
        PlayerController.Disable();
    }
}
