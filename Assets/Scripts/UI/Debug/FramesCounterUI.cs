using System.Collections;
using TMPro;
using UnityEngine;

public class FramesCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI framesPerSecond;
    private void OnEnable()
    {
        StartCoroutine(C_CountFrames());
    }
    private IEnumerator C_CountFrames()
    {
        int frames;
        float status = 1;
        while (isActiveAndEnabled)
        {
            frames = 0;
            status -= 1;
            while(status < 1)
            {
                yield return null;
                float delta = Time.unscaledDeltaTime;
                status += delta;
                frames++;
            }
            if (framesPerSecond != null) framesPerSecond.text = frames.ToString();
        }
    }
}
