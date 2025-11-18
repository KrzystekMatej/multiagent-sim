
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransitionTemplate))]
public class TransitionTemplateEditor : Editor
{
    private string[] stateTypes;
    private string[] transitionTypes;
    private int selectedStateIndex;
    private int selectedTransitionIndex;

    private void OnEnable()
    {
        stateTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(State)) && !t.IsAbstract)
            .Select(t => t.FullName)
            .ToArray();

        transitionTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(StateTransition)) && !t.IsAbstract)
            .Select(t => t.FullName)
            .ToArray();
    }

    public override void OnInspectorGUI()
    {
        var template = (TransitionTemplate)target;

        selectedStateIndex = Array.IndexOf(stateTypes, template.TargetState);
        if (selectedStateIndex < 0) selectedStateIndex = 0;

        selectedTransitionIndex = Array.IndexOf(transitionTypes, template.Transition);
        if (selectedTransitionIndex < 0) selectedTransitionIndex = 0;

        EditorGUILayout.LabelField("Transition Template", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("Target State:");
        selectedStateIndex = EditorGUILayout.Popup(selectedStateIndex, stateTypes);
        template.TargetState = stateTypes.Length > 0 ? stateTypes[selectedStateIndex] : "";

        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("Transition:");
        selectedTransitionIndex = EditorGUILayout.Popup(selectedTransitionIndex, transitionTypes);
        template.Transition = transitionTypes.Length > 0 ? transitionTypes[selectedTransitionIndex] : "";

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("Select the target state and transition type from the available project classes.", MessageType.Info);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(template);
        }
    }
}
