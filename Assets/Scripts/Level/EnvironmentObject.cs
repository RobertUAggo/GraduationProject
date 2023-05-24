using UnityEngine;
using UnityEngine.EventSystems;

public class EnvironmentObject : MonoBehaviour, IDragHandler
{
    public string Name;
    public Sprite Sprite;
    public int ObjectId { get; set; }

    private Ray GetRay(Vector2 screenPos)
    {
        return new Ray(Camera.main.transform.position,
            Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f))
            - Camera.main.transform.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = GetRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
            1 << LayersManager.Ground))
        {
            transform.position = hit.point;
        }
    }
}