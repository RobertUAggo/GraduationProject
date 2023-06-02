using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailReset : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    private void OnEnable()
    {
        trailRenderer.Clear();
    }
}
