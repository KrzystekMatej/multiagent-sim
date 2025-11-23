using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInputProvider : AgentComponent
{
    [SerializeField]
    protected AgentInputData inputData = new AgentInputData();
    public AgentInputData InputData { get => inputData; }

    private AIAgentController defaultController;

    public override void Initialize(AgentContext agent)
    {
        defaultController = agent.Get<AIAgentController>();
        ResetSource();
    }

    public void SetSource(IAgentInputSource controller)
    {
        inputData = controller.ProvideInputSource();
    }

    public void ResetSource()
    {
        if (defaultController != null)
            SetSource(defaultController);
    }
}
