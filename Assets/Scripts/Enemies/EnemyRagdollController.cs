using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdollController : MonoBehaviour
{
    // ragdoll components
    [SerializeField] private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();
    private List<Collider> _colliders = new List<Collider>();

    // non-ragdoll components
    private Rigidbody mainRigidbody;
    private Collider mainHitbox;
    private Animator animator;

    private bool _isRagdoll;
    public bool IsRagdoll() => _isRagdoll;

    // returns a single rigidbody component from the ragdoll, usually the hips,
    // so the calling function can apply force to it. A calling function will do this
    // instead of applying force to all ragdoll body parts for performance. 
    //
    // for example, an explosion would cast OverlapSphere and enable the ragdoll on
    // an enemy. The OverlapShere would not detect the ragdoll components so it can
    // instead request one component from the ragdoll to apply force to it. Even though
    // the other parts don't have force applied to it, they will stil react in a natural
    // way as the single body part is affected. Hips is usually the best part to return.
    //
    // _rigidbodies[0] MUST be tagged as an enemey.
    public Rigidbody GetRagdollRigidbody() => _rigidbodies[0];

    void Start()
    {
        mainRigidbody = GetComponent<Rigidbody>();
        mainHitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        foreach (GameObject bodyPart in _bodyParts)    
        {
            _rigidbodies.Add(bodyPart.GetComponent<Rigidbody>());
            _colliders.Add(bodyPart.GetComponent<Collider>());
        }

        _isRagdoll = true;
        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        if (!_isRagdoll)
        {
            foreach (Rigidbody rb in _rigidbodies)            
            {
                rb.isKinematic = false;
            }

            foreach (Collider col in _colliders)
            {
                col.enabled = true;
            }

            mainRigidbody.isKinematic = true;
            mainHitbox.enabled = false;
            animator.enabled = false;
            _isRagdoll = true;
        }
    }

    public void DisableRagdoll()
    {
        if (_isRagdoll)
        {
            _isRagdoll = false;
            mainRigidbody.isKinematic = false;
            mainHitbox.enabled = true;
            animator.enabled = true;

            foreach (Rigidbody rb in _rigidbodies)            
            {
                rb.isKinematic = true;
            }

            foreach (Collider col in _colliders)
            {
                col.enabled = false;
            }
        }
    }

    public void Update()
    {
        
    }
}
