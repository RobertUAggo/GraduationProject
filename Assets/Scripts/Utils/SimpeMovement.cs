using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpeMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSensetivity = 10;
    [SerializeField] private float speed = 10;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = (Input.GetAxis("Horizontal") * transform.right 
            + Input.GetAxis("Vertical") * transform.forward) * speed;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSensetivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSensetivity * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        cameraTransform.rotation *= Quaternion.Euler(mouseY, 0, 0);
    }
}
