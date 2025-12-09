using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField]
    private InventoryItem item;

    [SerializeField]
    private int quantity;

    public InventoryItem Item => item;
    public int Quantity => quantity;
    public bool IsEmpty => quantity <= 0;

    public void Initialize(InventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddQuantity(int value)
    {
        quantity += value;
    }

    public void SubtractQuantity(int value)
    {
        quantity -= value;
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
