using UnityEngine;
using SimpleFileBrowser;

public class MainUI : MonoBehaviour
{
    public LoadScreenUI LoadScreenUI;
    public Canvas Canvas { get; private set; }
    public void Init()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("JSON", ".json"));
        //FileBrowser.DisplayedEntriesFilter += (entry) =>
        //{
        //    if (entry.IsDirectory)
        //        return true;
        //    return entry.Extension == ".json";
        //};
        Canvas = GetComponent<Canvas>();
        LoadScreenUI.Init();
    }
}
