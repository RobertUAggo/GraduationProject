using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    Menu = 1,
    Creator = 2,
    Level = 3,
}

public class SceneLoader : MonoBehaviour
{
    public string LevelDataFilePath;
    private GameScene _currentScene;
    public void Init()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    public void LoadScene(GameScene scene)
    {
        SceneManager.UnloadSceneAsync((int)_currentScene);
        _currentScene = scene;
        var loadOperation = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        StartCoroutine(C_LoadProcess(loadOperation));
    }
    private IEnumerator C_LoadProcess(AsyncOperation loadOperation)
    {
        Main.Instance.MainUI.LoadScreenUI.Show();
        loadOperation.allowSceneActivation = false;
        while (loadOperation.isDone == false)
        {
            Main.Instance.MainUI.LoadScreenUI.SetProgress(loadOperation.progress);
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
        Main.Instance.MainUI.LoadScreenUI.Hide();
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
#if UNITY_EDITOR
    public void EditorSetCurrentScene(GameScene scene)
    {
        _currentScene = scene;
    }
#endif
}
