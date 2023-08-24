using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFireCaster : SpellCaster
{
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private GameObject _holyFirePrefab;

    private GameObject _indicator;
    private bool _castRay;
    private bool _castingSpell;
    private bool _handUp;

    private float _startYPos;
    private float _yPosThreshold = 0.5f;

    public override void EquipSpell()
    {
        _castRay = true;
    }

    public override void UnequipSpell()
    {
        _castRay = false;
        if (_indicator) 
        {
            Destroy(_indicator);
        }
    }

    public override void StartCast()
    {
        _startYPos = _hand.Grabber.transform.position.y;

        if (_indicator) 
        {
            _castRay = false;
            _castingSpell = true;
        }
    }

    public override void StopCast() 
    { 
        _castRay = true;
    }

    void Update()
    {
        // Cast ray and spawn indicator on ground.
        if (_castRay) 
        {
            Ray ray = new Ray(_hand.Grabber.transform.position, _hand.Grabber.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                // Connects with ground layer
                if (hit.collider.gameObject.layer == 6) 
                {
                    if (!_indicator) 
                    {
                        _indicator = Instantiate(_indicatorPrefab, hit.point + 
                            new Vector3(0, .1f, 0), Quaternion.Euler(90, 0, 0));
                    }
                    else 
                    {
                        _indicator.transform.position = hit.point;
                    }
                }
                else 
                {
                    Destroy(_indicator);
                }
            }
        }

        if (_castingSpell)
        {
            // Raise hand up.
            if (!_handUp && _hand.Grabber.transform.position.y > _startYPos + _yPosThreshold)
            {
                _handUp = true;
            }
            // Bring hand down.
            else if (_handUp && _hand.Grabber.transform.position.y < _startYPos)
            {
                _castingSpell = false;

                Instantiate(_holyFirePrefab, _indicator.transform.position, Quaternion.Euler(-90, 0, 0));
                Destroy(_indicator);
            }
        }
    }
}
