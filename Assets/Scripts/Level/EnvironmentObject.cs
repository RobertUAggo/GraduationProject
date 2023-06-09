﻿using UnityEngine;
using UnityEngine.EventSystems;

public class EnvironmentObject : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    private Collider _collider;
    public string Name;
    public Sprite Sprite;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    public int ObjectId { get; set; }
    //
    private Ray GetRay(Vector2 screenPos)
    {
        return new Ray(Camera.main.transform.position,
            Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f))
            - Camera.main.transform.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        switch (Creator.Instance.Mode)
        {
            case CreatorMode.Position:
                Ray ray = GetRay(eventData.position);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
                    1 << LayersManager.Ground))
                {
                    transform.position = hit.point;
                }
                break;
            case CreatorMode.Rotation:
                transform.Rotate(0, eventData.delta.x / Main.Instance.MainUI.Canvas.scaleFactor, 0);
                break;
        }
    }
    public void SetColliderAsTrigger()
    {
        _collider.isTrigger = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (Creator.Instance.Mode)
        {
            case CreatorMode.Removing:
                Creator.Instance.EnvironmentManager.RemoveObject(this);
                break;
        }
    }
}