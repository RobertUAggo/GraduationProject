public class Creator : MonoSingleton<Creator>
{
    public EnvironmentManager EnvironmentManager;
    public CreatorUI CreatorUI;
    public CameraMover CameraMover;
    public CreatorMode Mode = CreatorMode.Camera;
    //
    private void Awake()
    {
        SingletonInit();
        EnvironmentManager.Init();
        CreatorUI.Init();
#if UNITY_EDITOR
        Main.Instance.SceneLoader.EditorSetCurrentScene(GameScene.Creator);
#endif
    }
}
