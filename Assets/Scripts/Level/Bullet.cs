using System.Collections;
using System.Collections.Generic;
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
        if (other.TryGetComponent(out BaseCreature baseCreature))
        {
            baseCreature.TakeDamage(_shooter.Damage);
            gameObject.SetActive(false);
        }
    }
}
