using System.Collections;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] private Bullet prefab;
    [SerializeField] private int capacity = 15;
    [SerializeField] private float lifeTime = 2;
    [SerializeField] private float speed = 1;
    private Pool<Bullet> _pool;
    public void Init()
    {
        _pool = new Pool<Bullet>(prefab, transform, capacity);
        _pool.OnCreate.AddListener(newInstance =>
        {
            newInstance.gameObject.SetActive(false);
        });
        _pool.Init();
    }
    public void Shoot(BaseCreature shooter, Vector3 startPos, Vector3 direction)
    {
        direction.Normalize();
        var instance = _pool.Take();
        instance.Set(shooter);
        instance.transform.position = startPos;
        instance.gameObject.SetActive(true);
        StartCoroutine(C_Fly(instance, direction));
    }
    private IEnumerator C_Fly(Bullet instance, Vector3 direction)
    {
        float time = 0;
        while(time < lifeTime && instance.gameObject.activeSelf)
        {
            yield return null;
            time += Time.deltaTime;
            instance.transform.position += direction * speed * Time.deltaTime;
        }
        instance.gameObject.SetActive(false);
        _pool.BackToQueue(instance);
    }
}
