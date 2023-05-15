using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private string format = "{0}<sprite=0>";
    [SerializeField] private TextMeshProUGUI textField;
    private void OnEnable()
    {
        Set(Main.Instance.PlayerManager.PlayerData.Money);
        Main.Instance.PlayerManager.OnMoneyChange.AddListener(Set);
    }
    private void OnDisable()
    {
        Main.Instance.PlayerManager.OnMoneyChange.RemoveListener(Set);
    }
    private void Set(int value)
    {
        textField.text = string.Format(format, value);
    }
}
