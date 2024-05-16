using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<AllItems> _inventoryItems = new List<AllItems>(); // Our inventory items

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(AllItems item) // Add Items to inventory
    {
        if (!_inventoryItems.Contains(item))
        {
            _inventoryItems.Add(item);
        }
    }

    public void RemoveItem(AllItems item) // Remove Items to inventory
    {
        if (_inventoryItems.Contains(item))
        {
            _inventoryItems.Remove(item);
        }
    }

    public enum AllItems // All available inventory items in game
    {
       KeySnorkel,
       KeyTube
    }

    void Update()
    {
        if (InventoryManager.Instance._inventoryItems.Contains(AllItems.KeySnorkel))
        {
            KeyManager.isGameEndSnorkel = true;
        }

        if (InventoryManager.Instance._inventoryItems.Contains(AllItems.KeyTube))
        {
            KeyManager.isGameEndTube = true;
        }
    }
}
