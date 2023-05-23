using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

public class Main : MonoSingleton<Main>
{
    public MainUI MainUI;
    public ItemsManager ItemsManager;
    public SceneLoader SceneLoader;
    public PlayerManager PlayerManager;
    private void Awake()
    {
        SingletonInit();
        PlayerManager.Init();
        MainUI.Init();
        SceneLoader.Init();
        //Debug.Log(Main.Instance, Main.Instance.gameObject);
    }
    private void Start()
    {
#if UNITY_EDITOR
        if(SceneManager.sceneCount > 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
            return;
        }
#endif
        SceneLoader.LoadScene(GameScene.Menu);
    }
}
