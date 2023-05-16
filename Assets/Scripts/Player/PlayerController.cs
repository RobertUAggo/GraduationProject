using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] Joystick joystick;
    [SerializeField] private float baseSpeed = 10;
    public Player Player => player;
    public void Init()
    {
        
    }
    private void Update()
    {
        transform.position += joystick.Direction3D * baseSpeed * Time.deltaTime;
    }
}
