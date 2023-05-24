using UnityEngine;
using UnityEngine.EventSystems;

public class CreatorInputUI : MonoBehaviour, IDragHandler
{
    [SerializeField] private bool reverse = true;
    public void OnDrag(PointerEventData eventData)
    {
        Creator.Instance.CameraMover.Move((reverse ? -1 : 1) *
            (eventData.delta / Main.Instance.MainUI.Canvas.scaleFactor));
    }
}
