using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BaseCreature _shooter;
    public void Set(BaseCreature shooter)
    {
        _shooter = shooter;
        gameObject.layer = _shooter.gameObject.layer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Level.Instance.PlayerController.Player.gameObject)
        {
            Hit(Level.Instance.PlayerController.Player);
        }
        else if (other.TryGetComponent(out BaseCreature target))
        {
            Hit(target);
        }
    }
    private void Hit(BaseCreature target)
    {
        target.TakeDamage(_shooter.Damage);
        gameObject.SetActive(false);
    }
}
