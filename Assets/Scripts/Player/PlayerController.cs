using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] Joystick joystick;
    [SerializeField] private float baseSpeed = 4;
    public Player Player => player;
    private Rigidbody _rigidbody;
    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = joystick.Direction3D * baseSpeed;
        if(_rigidbody.velocity != Vector3.zero) _rigidbody.rotation = Quaternion.LookRotation(joystick.Direction3D);
        animator.SetBool(IsMovingParam, _rigidbody.velocity != Vector3.zero);
    }
}
