#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

public class Main : MonoSingleton<Main>
{
    public MainUI MainUI;
    public SceneLoader SceneLoader;
    private void Awake()
    {
        SingletonInit();
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
