using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public EnvironmentObject[] EnvironmentObjects;
    private void Awake()
    {
        for (int i = 0; i < EnvironmentObjects.Length; i++)
        {
            EnvironmentObjects[i].ObjectId = i;
        }
    }
}
