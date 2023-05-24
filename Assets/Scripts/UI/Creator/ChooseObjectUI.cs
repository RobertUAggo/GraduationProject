using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseObjectUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    private EnvironmentObject _environmentObject;
    public void Set(EnvironmentObject environmentObject)
    {
        _environmentObject = environmentObject;
        image.sprite = _environmentObject.Sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Click on {name}", gameObject);
        Creator.Instance.CreatorUI.ChoosePanelUI.Hide();
        Creator.Instance.EnvironmentManager.AddObject(_environmentObject.ObjectId,
            0, 0, //TODO
            0);
    }
}
