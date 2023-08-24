using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float _timeToDestruct = 5f;

    void Start()
    {
        Destroy(gameObject, _timeToDestruct);       
    }
}
