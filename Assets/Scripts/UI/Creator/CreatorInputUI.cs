using UnityEngine;
using UnityEngine.EventSystems;

public class CreatorInputUI : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Creator.Instance.CameraMover.Move(eventData.delta);
    }
}
