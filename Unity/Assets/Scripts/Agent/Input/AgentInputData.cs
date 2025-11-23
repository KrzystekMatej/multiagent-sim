using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInputData
{
    public Vector2 MoveInput;
    public InputState Jump;
    public InputState Attack;
    public InputState Run;
    public InputState Roll;
}

public enum InputState
{
    Inactive,
    Pressed,
    Held,
    Released
}
