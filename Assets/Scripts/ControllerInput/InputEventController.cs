using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Devices;
using UltimateXR.Core;

// Contains all the information of a specific input (side, button) and provides
// functions to subscribe other functions to the event and handles the invocation
// of said events.
public class InputEventController : MonoBehaviour
{
    public delegate void Pressed();
    public delegate void Released();

    private event Pressed _onPressed;
    private event Released _onReleased;

    private bool _pressed;

    [SerializeField] private UxrHandSide _handSide;
    [SerializeField] private UxrInputButtons _button;

    public UxrHandSide HandSide => _handSide;
    public UxrInputButtons Button => _button;

    // Subscribe a function to the event of this button press.
    public void SubscribePressed(Pressed func) 
    { 
        _onPressed += func; 
    }

    // Subscribe a function to the event of this button release.
    public void SubscribeReleased(Released func) 
    { 
        _onReleased += func; 
    }

    public void SubscribePressedAndReleased(Pressed pressFunc, Released releaseFunc)
    {
        _onPressed += pressFunc; 
        _onReleased += releaseFunc; 
    }

    void Update()
    {
        // Poll for this this button press.
        if (!_pressed && 
            UxrAvatar.LocalAvatarInput.GetButtonsPressDown(_handSide, _button)) 
        {
            _pressed = true;
            _onPressed?.Invoke();
        }

        // Poll for this this button release.
        if (_pressed && 
            UxrAvatar.LocalAvatarInput.GetButtonsEvent(_handSide, _button, UxrButtonEventType.PressUp)) 
        {
            _pressed = false;
            _onReleased?.Invoke();
        }
    }
}