using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorUI : MonoBehaviour
{
    public ChoosePanelUI ChoosePanelUI;
    public void Init()
    {
        ChoosePanelUI.Init();
    }
    public void SaveClick()
    {
        Creator.Instance.EnvironmentManager.Save();
    }
    public void SaveAsClick()
    {

    }
}
