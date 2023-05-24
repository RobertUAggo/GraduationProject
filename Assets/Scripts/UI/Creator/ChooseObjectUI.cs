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
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Creator.Instance.EnvironmentManager.Plane.Raycast(ray, out float dist);
        Vector3 resultPos = ray.GetPoint(dist);
        //Debug.Log($"Click on {name}", gameObject);
        Creator.Instance.CreatorUI.ChoosePanelUI.Hide();
        Creator.Instance.EnvironmentManager.AddObject(_environmentObject.ObjectId,
            resultPos.x, resultPos.z,
            0);
    }
}
