using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBarUI : MonoBehaviour
{
    [SerializeField] private Image targetFillImage;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private string textFormat = "{0}";
    private bool _isShowingMax = false;
    private void Awake()
    {
        _isShowingMax = textFormat.Contains("{1}");
    }
    public void Set(float current, float max = 1)
    {
        targetFillImage.fillAmount = current / max;
        if(_isShowingMax == false)
            textField.text = string.Format(textFormat, current);
        else
            textField.text = string.Format(textFormat, current, max);
    }
}
