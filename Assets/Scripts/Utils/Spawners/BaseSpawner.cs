using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public virtual Vector3 GetPoint()
    {
        return transform.position;
    }
}
