using System.Collections;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private int capacity = 15;
    [SerializeField] private float lifeTime = 2;
    [SerializeField] private float speed = 1;
    private Pool<Transform> _pool;
    public void Init()
    {
        _pool = new Pool<Transform>(bulletPrefab, transform, capacity);
        _pool.OnCreate.AddListener(newInstance =>
        {
            newInstance.gameObject.SetActive(false);
        });
        _pool.Init();
    }
    public void Shoot(Vector3 startPos, Vector3 direction)
    {
        direction.Normalize();
        var instance = _pool.Take();
        instance.position = startPos;
        instance.gameObject.SetActive(true);
        StartCoroutine(C_Fly(instance, direction));
    }
    private IEnumerator C_Fly(Transform instance, Vector3 direction)
    {
        float time = 0;
        while(time < lifeTime)
        {
            yield return null;
            time += Time.deltaTime;
            instance.position += direction * speed * Time.deltaTime;
        }
        instance.gameObject.SetActive(false);
        _pool.BackToQueue(instance);
    }
}
