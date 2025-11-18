using System;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/Transition Template")]
public class TransitionTemplate : ScriptableObject
{
    [HideInInspector] public string TargetState;
    [HideInInspector] public string Transition;
}