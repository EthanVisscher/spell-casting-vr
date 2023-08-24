using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    private bool _isDead;

    public void ApplyDamage(float damage) 
    {
        _health -= damage;

        if (_health <= 0) kill();
    }

    private void kill()
    {
        _isDead = true;
    }
}
