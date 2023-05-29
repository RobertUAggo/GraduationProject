using UnityEngine;
using UnityEngine.Events;

public class AnimatorProvider : MonoBehaviour
{
    public UnityEvent[] Events;
    public void DoEvent(int i)
    {
        Events[i].Invoke();
    }
}