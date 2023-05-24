using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatorInputUI : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    private EnvironmentObject _currentEnvironmentObject = null;

    private Ray GetRay(Vector2 screenPos)
    {
        return new Ray(Camera.main.transform.position,
            Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f))
            - Camera.main.transform.position);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = GetRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
            1 << LayersManager.EnvironmentObject)
            && hit.collider.gameObject.TryGetComponent(out EnvironmentObject envObj))
        {
            if (_currentEnvironmentObject == envObj)
            {
                Debug.DrawLine(ray.origin, hit.point, Color.yellow, 5f);
                //_currentEnvironmentObject.RemoveSelection();
                _currentEnvironmentObject = null;
            }
            else
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 5f);
                _currentEnvironmentObject = envObj;
                //_currentEnvironmentObject.SetSelected();
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction * 25, Color.red, 5f);
            OnDrag(eventData);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_currentEnvironmentObject != null)
        {
            Ray ray = GetRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
                1 << LayersManager.Ground))
            {
                _currentEnvironmentObject.transform.position = hit.point;
            }
        }
        else
        {
            Creator.Instance.CameraMover.Move(eventData.delta);
        }
    }
}
