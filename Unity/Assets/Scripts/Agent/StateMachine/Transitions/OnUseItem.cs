using UnityEngine;

[System.Serializable]
public class OnUseItem : StateTransition
{
    private AgentInputProvider inputProvider;
    public OnUseItem(State target) : base(target) { }

    public override void Initialize(AgentContext agentContext)
    {
        inputProvider = agentContext.Get<AgentInputProvider>();
    }
    public override bool IsTriggered(AgentContext agent)
    {
        if (inputProvider.InputData.UseItem != InputState.Pressed) return false;
        InventoryItem item = agent.Get<Inventory>().SelectedSlot.Item;

        if (item == null || !item.IsUsable) return false;

        switch (item.Type)
        {
            case ItemType.Hammer:
                Target = agent.Get<BuildState>();
                break;
            case ItemType.Axe:
                Target = agent.Get<ChopState>();
                break;
            case ItemType.Sword:
                Target = agent.Get<AttackState>();
                break;
            case ItemType.Shovel:
                Target = agent.Get<DigState>();
                break;
        }

        return true;
    }
}
