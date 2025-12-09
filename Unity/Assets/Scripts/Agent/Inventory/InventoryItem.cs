using UnityEngine;

public enum ItemType
{
    Axe,
    Hammer,
    Sword,
    Shovel
}

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    private string displayName;
    [SerializeField]
    private ItemType type;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private bool isStackable = true;
    [SerializeField]
    private int maximumStackSize = 16;
    [SerializeField]
    private bool isUsable = false;

    public string DisplayName => displayName;
    public ItemType Type => type;
    public Sprite Icon => icon;
    public bool IsStackable => isStackable;
    public int MaximumStackSize => maximumStackSize;
    public bool IsUsable => isUsable;
}
