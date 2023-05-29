using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SkinData
{
    public string Name;
    public Sprite Sprite;
    [TextArea] public string Description;
    public GameObject[] GameObjects;
    public UnityEvent OnStart;
}