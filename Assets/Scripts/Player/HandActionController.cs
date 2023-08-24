using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Avatar.Controllers;
using UltimateXR.Devices;
using UltimateXR.Core;
using UltimateXR.Manipulation;
using UltimateXR.Haptics;

public class HandActionController : MonoBehaviour
{
    [SerializeField] private UxrHandSide _handSide;
    [SerializeField] private InputEventManager _inputEventManager;

    [SerializeField] private Transform _spellCastersTransform;
    private Dictionary<string, SpellCaster> _spells;
    private SpellCaster _equippedSpell;
    private SpellCaster _spell1;
    private SpellCaster _spell2;

    private bool _casting = false; 
    private bool _gripping = false; 

    public UxrHandSide HandSide => _handSide; 
    public UxrGrabber Grabber => UxrAvatar.LocalAvatar.GetGrabber(_handSide); 

    void OnEnable()
    {
        _inputEventManager.GetTrigEventController(_handSide).SubscribePressedAndReleased(CastSpell, StopSpell);
        _inputEventManager.GetGripEventController(_handSide).SubscribePressedAndReleased(GripPressed, GripReleased);
        _inputEventManager.GetBtn1EventController(_handSide).SubscribePressed(EquipSpell1);
        _inputEventManager.GetBtn2EventController(_handSide).SubscribePressed(EquipSpell2);
    }

    void Start()
    {
        // Populate spell casters dictionary. 
        _spells = new Dictionary<string, SpellCaster>();
        foreach (Transform spellCasterTransform in _spellCastersTransform)
        {
            _spells[spellCasterTransform.gameObject.name] = spellCasterTransform.gameObject.GetComponent<SpellCaster>();
            _spells[spellCasterTransform.gameObject.name].SetHand(this);
        }

        _spell1 = _spells["Fireball"];
        if (_handSide == UxrHandSide.Left) 
        {
            _spell2 = _spells["BoneCage"];
        }
        else 
        {
            _spell2 = _spells["HolyFire"];
        }
    }

    private void changePose()
    {
        string poseName;

        // Get hand pose name.
        if (_casting)
        {
            poseName = "Cast" + _equippedSpell.Name;
        }
        else if (_equippedSpell)
        {
            poseName = "Equip" + _equippedSpell.Name;
        }
        else 
        {
            poseName = null;
        }

        UxrStandardAvatarController avatarController = UxrAvatar.LocalAvatar.AvatarController 
            as UxrStandardAvatarController;

        // Change hand pose.
        if (_handSide == UxrHandSide.Left) 
        {
            avatarController.LeftHandDefaultPoseNameOverride = poseName;
        }
        else 
        {
            avatarController.RightHandDefaultPoseNameOverride = poseName;
        }
    }

    private void CastSpell()
    {
        if (_gripping || !_equippedSpell) return;

        _casting = true;
        changePose();
        _equippedSpell.StartCast();
        //UxrAvatar.LocalAvatar.ControllerInput.SendHapticFeedback(UxrHandSide.Left, UxrHapticClipType.RumbleFreqNormal, 1.0f);
    }

    private void StopSpell()
    {
        if (_gripping || !_equippedSpell) return;

        _casting = false;
        changePose();
        _equippedSpell.StopCast();
    }

    private void GripPressed()
    {
        if (_casting) return;

        _gripping = true;
    }

    private void GripReleased()        
    {
        if (_casting) return;

        _gripping = false;
    }

    private void equipSpell(SpellCaster newSpell)
    {
        if (_equippedSpell) 
        {
            _equippedSpell.UnequipSpell();
        }

        // new spell already equipped, unequip
        if (_equippedSpell == newSpell) 
        {
            _equippedSpell = null;
        }
        else 
        {
            _equippedSpell = newSpell;
            _equippedSpell.EquipSpell();
        }

        changePose();
    }

    private void EquipSpell1()
    {
        if (_casting) return;

        equipSpell(_spell1);
    }

    private void EquipSpell2()
    {
        if (_casting) return;

        equipSpell(_spell2);
    }
}