using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public EnvironmentObject[] EnvironmentObjects;
    public void Init()
    {
        for (int i = 0; i < EnvironmentObjects.Length; i++)
        {
            EnvironmentObjects[i].ObjectId = i;
        }
    }
}
