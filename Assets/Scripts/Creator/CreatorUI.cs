using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorUI : MonoBehaviour
{
    [SerializeField] private GameObject mainButtonsPanel;
    public void SaveClick()
    {
        Creator.Instance.EnvironmentManager.Save();
    }
    public void SaveAsClick()
    {

    }
}
