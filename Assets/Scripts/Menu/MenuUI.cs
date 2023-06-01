using UnityEngine;
using SimpleFileBrowser;

public class MenuUI : MonoBehaviour
{
    public ShopUI ShopUI;
    public SettingsUI SettingsUI;
    public void Init()
    {
        SettingsUI.Init();
        ShopUI.Init();
    }
    public void PlayClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Level);
    }
    public void CreatorNewClick()
    {
        Main.Instance.SceneLoader.LoadScene(GameScene.Creator);
    }
    public void CreatorOpenClick()
    {
        FileBrowser.ShowLoadDialog(success =>
        {
            Main.Instance.SceneLoader.LevelDataFilePath = success[0];
            Main.Instance.SceneLoader.LoadScene(GameScene.Creator);
        },
        () => FileBrowser.HideDialog(),
        FileBrowser.PickMode.Files);
    }
}
