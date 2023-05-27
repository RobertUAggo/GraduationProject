using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] private float hideTime = 0.5f;
    [SerializeField] private int capacity = 35;
    [SerializeField] private TextMeshProUGUI prefab;
    private Pool<TextMeshProUGUI> _pool;
    public void Init()
    {
        _pool = new Pool<TextMeshProUGUI>(prefab, transform, capacity);
        _pool.OnCreate.AddListener(OnCreate);
        _pool.Init();
    }
    private void OnCreate(TextMeshProUGUI instance)
    {
        instance.gameObject.SetActive(false);
    }
    public void Show(string text, Vector3 position)
    {
        var instance = _pool.Take();
        instance.text = text;
        instance.transform.position = position;
        instance.gameObject.SetActive(true);
        StartCoroutine(C_HideAfter(instance));
    }
    private IEnumerator C_HideAfter(TextMeshProUGUI instance)
    {
        yield return new WaitForSeconds(hideTime);
        instance.gameObject.SetActive(false);
        _pool.BackToQueue(instance);
    }
}
