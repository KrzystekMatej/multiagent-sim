using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInputData
{
    public Vector2 MoveInput;
    public InputState Jump;
    public InputState Run;
    public InputState Roll;
    public InputState UseItem;
}

public enum InputState
{
    Inactive,
    Pressed,
    Held,
    Released
}
