using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;
using UltimateXR.Core;

public class FireballCaster : SpellCaster 
{
    [SerializeField] private GameObject _castedFireballPrefab;
    private GameObject _castedFireball;
    //private PlayerVelocity _playerVelocity;

    private float _velocityMultiplier = 1.4f;

    public void Start()
    {
        //_playerVelocity = transform.root.GetComponent<PlayerVelocity>();
    }

    // Spell doesn't use.
    public override void EquipSpell() {}
    public override void UnequipSpell() {}

    public override void StartCast()
    {
        // Spawn fireball in hand
        float yOffset = (_hand.HandSide == UxrHandSide.Left) ? 0.05f : -0.05f;
        _castedFireball = Instantiate(_castedFireballPrefab, _hand.Grabber.transform.position, 
            _hand.Grabber.transform.rotation); 
        _castedFireball.transform.SetParent(_hand.Grabber.transform);
        _castedFireball.transform.localPosition = new Vector3(0, yOffset, 0);
    }

    public override void StopCast()
    {
        // Throw fireball
        if (_castedFireball)
        {
            // Release and activate rigidbody
            _castedFireball.transform.SetParent(null);
            _castedFireball.GetComponent<FireballController>().Cast();
            Rigidbody rb = _castedFireball.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            // add hand and body velocity
            //rb.AddForce((_hand.Grabber.SmoothVelocity) * _velocityMultiplier, ForceMode.Impulse);
            //rb.AddTorque(_hand.Grabber.SmoothAngularVelocity, ForceMode.Impulse);
        }
    }
}
