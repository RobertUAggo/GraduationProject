using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySkinUI : MonoBehaviour
{
    [SerializeField] private int price = 100;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTextField;
    [SerializeField] private TextMeshProUGUI infoTextField;
    [SerializeField] private TextMeshProUGUI priceTextField;
    [SerializeField] private GameObject ownedObj;
    [SerializeField] private GameObject equipedObj;
    private int _skinId;
    public void Set(int skinId)
    {
        _skinId = skinId;
        var skinData = Menu.Instance.Player.SkinManager.Skins[skinId];
        image.sprite = skinData.Sprite;
        nameTextField.text = skinData.Name;
        infoTextField.text = skinData.Description;
        priceTextField.text = $"{price}<sprite=0>";
        ownedObj.SetActive(Main.Instance.PlayerManager.PlayerData.OwnedSkins[_skinId]);
        SetEquip(_skinId == Main.Instance.PlayerManager.PlayerData.CurrentSkin);
    }
    public void BuyClick()
    {
        if (price > Main.Instance.PlayerManager.PlayerData.Money)
        {
            return;
        }
        Main.Instance.PlayerManager.ChangeMoney(-price);
        Main.Instance.PlayerManager.PlayerData.OwnedSkins[_skinId] = true;
        Main.Instance.PlayerManager.Save();
        ownedObj.SetActive(true);
    }
    public void EquipClick()
    {
        Menu.Instance.MenuUI.ShopUI.EquipSkin(_skinId);
        Menu.Instance.Player.SkinManager.SetSkin(_skinId);
    }
    public void SetEquip(bool status)
    {
        equipedObj.SetActive(status);
    }
}
