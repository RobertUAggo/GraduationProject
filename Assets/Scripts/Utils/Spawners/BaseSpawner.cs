using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public virtual Vector3 GetRandPoint()
    {
        return transform.position;
    }
}
