using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    [SerializeField] private GameObject _explosionFXPrefab;
    [SerializeField] private GameObject _trailFXPrefab;

    private float _explosionRadius = 3f; 
    private float _explosionForce = 500f;
    private float _damage = 50f;

    private Vector3 _startPos;
    private bool _spawnedTrail = false;
    private bool _casted = false;

    public void Cast()
    {
        _casted = true;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        // wait until fireball move a bit to spawn trail FX so the trail doesn't
        // flash bang the user
        if (!_spawnedTrail && _casted && Vector3.Distance(transform.position, _startPos) >= 0.75f)
        {
            _spawnedTrail = true;
            GameObject trail = Instantiate(_trailFXPrefab, transform.position, Quaternion.identity);
            trail.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(_explosionFXPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(_explosionRadius + 1f, _explosionRadius + 1f, _explosionRadius + 1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Rigidbody"))
            {
                var rb = collider.GetComponent<Rigidbody>();
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }

            if (collider.CompareTag("Enemy"))
            {
                var health = collider.transform.root.GetComponent<EnemyHealthController>(); 
                var ragdoll = collider.transform.root.GetComponent<EnemyRagdollController>();

                health.ApplyDamage(_damage);
                ragdoll.EnableRagdoll();
                ragdoll.GetRagdollRigidbody().AddExplosionForce(_explosionForce * 10f, transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}
