using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 10;
    public Player Player { get; private set; }
    private void Awake()
    {
        Player = GetComponentInChildren<Player>();
    }
    public void Init()
    {
        
    }
    private void Update()
    {
        transform.position += Level.Instance.LevelUI.Joystick.Direction3D * baseSpeed * Time.deltaTime;
    }
}
