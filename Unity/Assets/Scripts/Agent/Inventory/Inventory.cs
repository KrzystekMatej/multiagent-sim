using System;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] itemSlots;

    [SerializeField]
    private UnityEvent inventoryChanged;

    public InventorySlot[] ItemSlots => itemSlots;
    public int SelectedSlotIndex { get; set; } = 0;

    public InventorySlot SelectedSlot => ItemSlots[SelectedSlotIndex]; 

    public int AddItem(InventoryItem inventoryItem, int quantity)
    {
        if (inventoryItem == null)
        {
            return 0;
        }

        if (quantity <= 0)
        {
            return 0;
        }

        int quantityToDistribute = quantity;
        int quantityAdded = 0;

        if (inventoryItem.IsStackable)
        {
            for (int index = 0; index < itemSlots.Length; index++)
            {
                InventorySlot slot = itemSlots[index];

                if (slot.IsEmpty)
                {
                    continue;
                }

                if (slot.Item != inventoryItem)
                {
                    continue;
                }

                if (slot.Quantity >= inventoryItem.MaximumStackSize)
                {
                    continue;
                }

                int freeSpace = inventoryItem.MaximumStackSize - slot.Quantity;
                int amountToAdd = quantityToDistribute <= freeSpace ? quantityToDistribute : freeSpace;

                slot.AddQuantity(amountToAdd);

                quantityToDistribute -= amountToAdd;
                quantityAdded += amountToAdd;

                if (quantityToDistribute <= 0)
                {
                    break;
                }
            }

            if (quantityToDistribute > 0)
            {
                for (int index = 0; index < itemSlots.Length; index++)
                {
                    InventorySlot slot = itemSlots[index];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    int stackQuantity = quantityToDistribute <= inventoryItem.MaximumStackSize
                        ? quantityToDistribute
                        : inventoryItem.MaximumStackSize;

                    slot.Initialize(inventoryItem, stackQuantity);

                    quantityToDistribute -= stackQuantity;
                    quantityAdded += stackQuantity;

                    if (quantityToDistribute <= 0)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            for (int index = 0; index < itemSlots.Length; index++)
            {
                if (quantityToDistribute <= 0)
                {
                    break;
                }

                InventorySlot slot = itemSlots[index];

                if (!slot.IsEmpty)
                {
                    continue;
                }

                slot.Initialize(inventoryItem, 1);

                quantityToDistribute -= 1;
                quantityAdded += 1;
            }
        }

        if (quantityAdded > 0 && inventoryChanged != null)
        {
            inventoryChanged.Invoke();
        }

        return quantityAdded;
    }

    public bool TryRemoveItem(InventoryItem inventoryItem, int quantity)
    {
        if (inventoryItem == null)
        {
            return false;
        }

        if (quantity <= 0)
        {
            return false;
        }

        int totalQuantity = GetTotalQuantity(inventoryItem);

        if (totalQuantity < quantity)
        {
            return false;
        }

        int remainingQuantity = quantity;

        for (int index = 0; index < itemSlots.Length; index++)
        {
            InventorySlot slot = itemSlots[index];

            if (slot.IsEmpty)
            {
                continue;
            }

            if (slot.Item != inventoryItem)
            {
                continue;
            }

            if (slot.Quantity <= remainingQuantity)
            {
                remainingQuantity -= slot.Quantity;
                slot.Clear();
            }
            else
            {
                slot.SubtractQuantity(remainingQuantity);
                remainingQuantity = 0;
            }

            if (remainingQuantity <= 0)
            {
                break;
            }
        }

        if (inventoryChanged != null)
        {
            inventoryChanged.Invoke();
        }

        return true;
    }

    public int GetTotalQuantity(InventoryItem inventoryItem)
    {
        if (inventoryItem == null)
        {
            return 0;
        }

        if (itemSlots == null)
        {
            return 0;
        }

        int totalQuantity = 0;

        for (int index = 0; index < itemSlots.Length; index++)
        {
            InventorySlot slot = itemSlots[index];

            if (slot.IsEmpty)
            {
                continue;
            }

            if (slot.Item != inventoryItem)
            {
                continue;
            }

            totalQuantity += slot.Quantity;
        }

        return totalQuantity;
    }

    public bool ContainsItem(InventoryItem inventoryItem, int minimumQuantity)
    {
        if (minimumQuantity <= 0)
        {
            return false;
        }

        int totalQuantity = GetTotalQuantity(inventoryItem);
        return totalQuantity >= minimumQuantity;
    }
}
