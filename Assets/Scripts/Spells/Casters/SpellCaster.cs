using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;

public abstract class SpellCaster : MonoBehaviour
{
    // Used to get the hand pose names for the spell.
    private string _name;

    public string Name => _name;

    // HandActionController is passed through instantiation, information
    // can be used while casting spells (hand velocity, position, etc).
    protected HandActionController _hand;

    void Start() 
    {
        _name = gameObject.name;
    }

    public void SetHand(HandActionController hand)
    {
        _hand = hand;
    }

    public abstract void EquipSpell();
    public abstract void UnequipSpell();
    public abstract void StartCast();
    public abstract void StopCast();
}
