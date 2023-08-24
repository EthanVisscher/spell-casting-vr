using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Core;

// Conviently stores all InputEventControllers registered and easily exposes
// them to other scripts.
public class InputEventManager : MonoBehaviour
{
    [SerializeField] private InputEventController _leftTrig;
    [SerializeField] private InputEventController _leftGrip;
    [SerializeField] private InputEventController _leftBtn1;
    [SerializeField] private InputEventController _leftBtn2;

    [SerializeField] private InputEventController _rightTrig;
    [SerializeField] private InputEventController _rightGrip;
    [SerializeField] private InputEventController _rightBtn1;
    [SerializeField] private InputEventController _rightBtn2;

    public InputEventController GetTrigEventController(UxrHandSide handSide)
    {
        return handSide == UxrHandSide.Left ? _leftTrig : _rightTrig;
    }

    public InputEventController GetGripEventController(UxrHandSide handSide)
    {
        return handSide == UxrHandSide.Left ? _leftGrip : _rightGrip;
    }

    public InputEventController GetBtn1EventController(UxrHandSide handSide)
    {
        return handSide == UxrHandSide.Left ? _leftBtn1 : _rightBtn1;
    }

    public InputEventController GetBtn2EventController(UxrHandSide handSide)
    {
        return handSide == UxrHandSide.Left ? _leftBtn2 : _rightBtn2;
    }
}
