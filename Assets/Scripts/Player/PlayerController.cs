using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private float _healthPoints = 100f; 
    private int _manaPoints = 5;
    private int _ultimatePoints = 0; 

    public int ManaPoints() => _manaPoints;

    public void ChangeManaPoints(int amount)
    {
        _manaPoints += amount;
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
