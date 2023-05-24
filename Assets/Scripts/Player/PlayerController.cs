using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] Joystick joystick;
    [SerializeField] private float baseSpeed = 10;
    public Player Player => player;
    private Rigidbody _rigidbody;
    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = joystick.Direction3D * baseSpeed;
    }
}
