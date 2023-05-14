using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    public void SingletonInit()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of {Instance.GetType().Name} already exists!");
        }
        //Instance = GetComponent<T>();
        Instance = this as T;

        Debug.Log($"MonoSingelton of {Instance.GetType().Name}");
    }
}
