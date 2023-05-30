using System.Collections;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] private Bullet prefab;
    [SerializeField] private int capacity = 15;
    [SerializeField] private float lifeTime = 2;
    [SerializeField] private float speed = 1;
    [SerializeField] private AnimationCurve curve;
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
    public void Shoot(BaseCreature shooter, int damage, Vector3 startPos, Vector3 direction)
    {
        direction.Normalize();
        var instance = _pool.Take();
        instance.Set(shooter, damage);
        instance.transform.position = startPos;
        instance.transform.forward = direction;
        instance.gameObject.SetActive(true);
        StartCoroutine(C_Fly(instance, direction));
    }
    private IEnumerator C_Fly(Bullet instance, Vector3 direction)
    {
        float time = 0;
        Vector3 linePos = instance.transform.position;
        while (time < lifeTime && instance.gameObject.activeSelf)
        {
            yield return null;
            time += Time.deltaTime;
            linePos += direction * speed * Time.deltaTime;
            instance.transform.position = linePos + instance.transform.right * curve.Evaluate(time);
        }
        instance.gameObject.SetActive(false);
        _pool.BackToQueue(instance);
    }
}
