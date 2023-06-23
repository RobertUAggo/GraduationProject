using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePanelUI : BaseUI
{
    [SerializeField] private ChooseObjectUI prefab;
    [SerializeField] private Transform root;
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public void Init()
    {
        gameObject.SetActive(false);
        foreach (Transform child in root.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var envObj in Main.Instance.ItemsManager.EnvironmentObjects)
        {
            var newChooseObjectUI = Instantiate(prefab, root);
            newChooseObjectUI.Set(envObj);
        }
    }
}
