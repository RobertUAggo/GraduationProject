using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private BaseCreature _shooter;
    private int _damage;
    public void Set(BaseCreature shooter, int damage)
    {
        _shooter = shooter;
        _damage = damage;
        //gameObject.layer = _shooter.gameObject.layer;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if(go == Level.Instance.PlayerController.Player.gameObject)
        {
            Level.Instance.PlayerController.Player.TakeDamage(_damage);
        }
        else if (go.TryGetComponent(out BaseCreature target))
        {
            target.TakeDamage(_damage);
        }
        gameObject.SetActive(false);
    }
}
