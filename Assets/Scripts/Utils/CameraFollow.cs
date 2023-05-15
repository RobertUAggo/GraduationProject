using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float smoothTime = 0.5f;
    private Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;
    private void Start()
    {
        _offset = transform.position - followTarget.transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            followTarget.transform.position + _offset,
            ref _velocity, smoothTime);
    }
}
